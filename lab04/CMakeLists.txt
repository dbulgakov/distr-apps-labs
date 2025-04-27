cmake_minimum_required(VERSION 3.20)

project(MPIExchange LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

if(UNIX AND NOT WIN32)
    list(PREPEND CMAKE_PREFIX_PATH "/opt/homebrew/opt/openmpi")
endif()

# Find MPI package
find_package(MPI REQUIRED)

add_executable(mpi_integral main.cpp)

# Include MPI headers
target_include_directories(mpi_integral PRIVATE ${MPI_INCLUDE_DIRS})

# Link with MPI libraries
target_link_libraries(mpi_integral PRIVATE MPI::MPI_CXX)

# On Windows, MS-MPI sometimes needs extra definition
if(WIN32)
    target_compile_definitions(mpi_integral PRIVATE _CRT_SECURE_NO_WARNINGS)
endif()
