#include <mpi.h>
#include <iostream>
#include <vector>

double factorial(int n) {
    double result = 1.0;
    for (int i = 2; i <= n; ++i) {
        result *= i;
    }
    return result;
}

double local_expansion(int start, int end) {
    double sum = 0.0;
    for (int i = start; i < end; ++i) {
        sum += 1.0 / factorial(i);
    }
    return sum;
}

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);

    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    std::vector<int> terms = {10, 100, 1000, 100000};

    for (int n : terms) {
        MPI_Barrier(MPI_COMM_WORLD);
        double start_time = MPI_Wtime();

        int local_n = n / size;
        int remainder = n % size;
        int start = rank * local_n + (rank < remainder ? rank : remainder);
        int end = start + local_n + (rank < remainder ? 1 : 0);

        double local_sum = local_expansion(start, end);

        double total_sum = 0.0;
        MPI_Reduce(&local_sum, &total_sum, 1, MPI_DOUBLE, MPI_SUM, 0, MPI_COMM_WORLD);

        MPI_Barrier(MPI_COMM_WORLD);
        double end_time = MPI_Wtime();

        if (rank == 0) {
            std::cout << "n = " << n << "\n";
            std::cout << "Approximate value of e^1: " << total_sum << "\n";
            std::cout << "Execution time: " << end_time - start_time << " seconds\n";
            std::cout << "-------------------------------------------\n";
        }
    }

    MPI_Finalize();
    return 0;
}
