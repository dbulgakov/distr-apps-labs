#include <mpi.h>
#include <array>
#include <cstdint>
#include <iostream>

constexpr std::int32_t BufLen = 256;
constexpr std::int32_t MsgTag = 100;

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);

    int myRank = 0;
    int numProcs = 0;
    MPI_Comm_rank(MPI_COMM_WORLD, &myRank);
    MPI_Comm_size(MPI_COMM_WORLD, &numProcs);

    if (numProcs != 3) {
        if (myRank == 0) {
            std::cerr << "Error: This program requires exactly 3 processes.\n";
        }
        MPI_Finalize();
        return EXIT_FAILURE;
    }

    std::array<char, BufLen> message{};
    MPI_Status status{};

    if (myRank == 2) { // Process 3
        std::snprintf(message.data(), message.size(), "Hello from process 3 to 2");
        MPI_Send(message.data(), static_cast<int>(std::strlen(message.data())) + 1, MPI_CHAR, 1, MsgTag, MPI_COMM_WORLD);

        MPI_Recv(message.data(), BufLen, MPI_CHAR, 1, MsgTag, MPI_COMM_WORLD, &status);
        std::cout << "Process 3 received message: " << message.data() << '\n';
    }
    else if (myRank == 1) { // Process 2
        MPI_Recv(message.data(), BufLen, MPI_CHAR, 2, MsgTag, MPI_COMM_WORLD, &status);
        std::cout << "Process 2 received message: " << message.data() << '\n';

        std::snprintf(message.data(), message.size(), "Hello from process 2 to 1");
        MPI_Send(message.data(), static_cast<int>(std::strlen(message.data())) + 1, MPI_CHAR, 0, MsgTag, MPI_COMM_WORLD);

        MPI_Recv(message.data(), BufLen, MPI_CHAR, 0, MsgTag, MPI_COMM_WORLD, &status);
        std::cout << "Process 2 received message: " << message.data() << '\n';

        std::snprintf(message.data(), message.size(), "Hello from process 2 to 3");
        MPI_Send(message.data(), static_cast<int>(std::strlen(message.data())) + 1, MPI_CHAR, 2, MsgTag, MPI_COMM_WORLD);
    }
    else if (myRank == 0) { // Process 1
        MPI_Recv(message.data(), BufLen, MPI_CHAR, 1, MsgTag, MPI_COMM_WORLD, &status);
        std::cout << "Process 1 received message: " << message.data() << '\n';

        std::snprintf(message.data(), message.size(), "Hello from process 1 to 2");
        MPI_Send(message.data(), static_cast<int>(std::strlen(message.data())) + 1, MPI_CHAR, 1, MsgTag, MPI_COMM_WORLD);
    }

    MPI_Finalize();
    return EXIT_SUCCESS;
}
