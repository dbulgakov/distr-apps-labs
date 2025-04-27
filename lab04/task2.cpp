#include <mpi.h>
#include <cmath>
#include <iostream>
#include <vector>

double f(double x) {
    return (x * x + 4 * x + 3) * std::cos(x);
}

double compute_integral(double a, double b, int n, int rank, int size) {
    double h = (b - a) / n;
    int local_n = n / size;
    double local_a = a + rank * local_n * h;
    double local_b = local_a + local_n * h;

    double local_sum = (f(local_a) + f(local_b)) / 2.0;
    for (int i = 1; i < local_n; ++i) {
        double x = local_a + i * h;
        local_sum += f(x);
    }
    local_sum *= h;

    double total_sum = 0.0;
    MPI_Reduce(&local_sum, &total_sum, 1, MPI_DOUBLE, MPI_SUM, 0, MPI_COMM_WORLD);

    return total_sum;
}

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);

    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    const double a = -1.0;
    const double b = 0.0;
    std::vector<int> subdivisions = {10, 100, 1000, 100000};

    for (int n : subdivisions) {
        MPI_Barrier(MPI_COMM_WORLD);
        double start_time = MPI_Wtime();

        double integral = compute_integral(a, b, n, rank, size);

        MPI_Barrier(MPI_COMM_WORLD);
        double end_time = MPI_Wtime();

        if (rank == 0) {
            std::cout << "n = " << n << "\n";
            std::cout << "Approximate integral value: " << integral << "\n";
            std::cout << "Execution time: " << end_time - start_time << " seconds\n";
            std::cout << "-------------------------------------------\n";
        }
    }

    MPI_Finalize();
    return 0;
}
