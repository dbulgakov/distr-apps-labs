#include <mpi.h>
#include <iostream>

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

    int n;
    if (rank == 0) {
        std::cout << "Input number of terms n: ";
        std::cin >> n;
    }

    MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
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
        std::cout << "Approximate value of e^1: " << total_sum << "\n";
        std::cout << "Execution time: " << end_time - start_time << " seconds\n";
    }

    MPI_Finalize();
    return 0;
}
