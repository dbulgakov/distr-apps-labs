cmake_minimum_required(VERSION 3.20)

project(MPIExchange LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

list(PREPEND CMAKE_PREFIX_PATH "/opt/homebrew/opt/openmpi")

find_package(MPI REQUIRED)

add_executable(mpi_exchange floyd_shortest.cpp)

target_include_directories(mpi_exchange PRIVATE ${MPI_INCLUDE_DIRS})
target_link_libraries(mpi_exchange PRIVATE MPI::MPI_CXX)
