cmake_minimum_required(VERSION 3.20)

project(MPIFloyd LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

if(UNIX AND NOT WIN32)
    list(PREPEND CMAKE_PREFIX_PATH "/opt/homebrew/opt/openmpi")
endif()

find_package(MPI REQUIRED)

set(MPI_PROCESSES 8 CACHE STRING "Number of MPI processes to use")

add_executable(mpi_floyd main.cpp)

target_include_directories(mpi_floyd PRIVATE ${MPI_INCLUDE_DIRS})
target_link_libraries(mpi_floyd PRIVATE MPI::MPI_CXX)

if(WIN32)
    target_compile_definitions(mpi_floyd PRIVATE _CRT_SECURE_NO_WARNINGS)
endif()

add_custom_target(run_mpi_floyd
        COMMAND ${MPIEXEC_EXECUTABLE} -n ${MPI_PROCESSES} $<TARGET_FILE:mpi_floyd>
        DEPENDS mpi_floyd
        WORKING_DIRECTORY ${CMAKE_RUNTIME_OUTPUT_DIRECTORY}
        COMMENT "Running mpi_floyd (Floyd-Warshall algorithm) with ${MPI_PROCESSES} processes"
)
