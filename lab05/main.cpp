#include <mpi.h>
#include <iostream>
#include <vector>
#include <iomanip>
#include <limits>

constexpr int INF = 1e9;

void print_matrix(const std::vector<std::vector<int>>& matrix) {
    int n = matrix.size();
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (matrix[i][j] >= INF / 2)
                std::cout << std::setw(5) << "inf";
            else
                std::cout << std::setw(5) << matrix[i][j];
        }
        std::cout << '\n';
    }
}

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);

    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    const int n = 7;

    std::vector<std::vector<int>> dist = {
        {0, 1, 2, 7, INF, 12, INF},
        {INF, 0, -1, 5, INF, INF, INF},
        {INF, INF, 0, 3, INF, 4, 6},
        {INF, 8, -4, 0, INF, INF, INF},
        {INF, 10, INF, 11, 0, INF, 9},
        {INF, INF, 13, INF, 14, 0, INF},
        {-5, 15, INF, -6, INF, 16, 0}
    };

    std::vector<int> flat_matrix(n * n);
    if (rank == 0) {
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                flat_matrix[i * n + j] = dist[i][j];
    }

    int rows_per_proc = n / size;
    if (n % size != 0) rows_per_proc += 1;
    std::vector<int> local_matrix(rows_per_proc * n, INF);

    MPI_Scatter(flat_matrix.data(), rows_per_proc * n, MPI_INT,
                local_matrix.data(), rows_per_proc * n, MPI_INT,
                0, MPI_COMM_WORLD);

    for (int k = 0; k < n; ++k) {
        int owner = k / rows_per_proc;

        std::vector<int> k_row(n, INF);
        if (rank == owner) {
            int local_k = k % rows_per_proc;
            for (int j = 0; j < n; ++j) {
                k_row[j] = local_matrix[local_k * n + j];
            }
        }

        MPI_Bcast(k_row.data(), n, MPI_INT, owner, MPI_COMM_WORLD);

        for (int i = 0; i < rows_per_proc; ++i) {
            int global_i = rank * rows_per_proc + i;
            if (global_i >= n) continue;
            for (int j = 0; j < n; ++j) {
                if (local_matrix[i * n + k] + k_row[j] < local_matrix[i * n + j]) {
                    local_matrix[i * n + j] = local_matrix[i * n + k] + k_row[j];
                }
            }
        }
    }

    MPI_Gather(local_matrix.data(), rows_per_proc * n, MPI_INT,
               flat_matrix.data(), rows_per_proc * n, MPI_INT,
               0, MPI_COMM_WORLD);

    if (rank == 0) {
        std::vector<std::vector<int>> result(n, std::vector<int>(n));
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                result[i][j] = flat_matrix[i * n + j];

        std::cout << "Resulting matrix of shortest paths:\n";
        print_matrix(result);
    }

    MPI_Finalize();
    return 0;
}
