cmake_minimum_required(VERSION 3.20)

project(MPIExchange LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

if(UNIX AND NOT WIN32)
    list(PREPEND CMAKE_PREFIX_PATH "/opt/homebrew/opt/openmpi")
endif()

find_package(MPI REQUIRED)

set(MPI_PROCESSES 3 CACHE STRING "Number of MPI processes to use")

add_executable(task1 task1.cpp)
add_executable(task2 task2.cpp)

target_include_directories(task1 PRIVATE ${MPI_INCLUDE_DIRS})
target_link_libraries(task1 PRIVATE MPI::MPI_CXX)

target_include_directories(task2 PRIVATE ${MPI_INCLUDE_DIRS})
target_link_libraries(task2 PRIVATE MPI::MPI_CXX)

if(WIN32)
    target_compile_definitions(task1 PRIVATE _CRT_SECURE_NO_WARNINGS)
    target_compile_definitions(task2 PRIVATE _CRT_SECURE_NO_WARNINGS)
endif()

add_custom_target(run_task1_mpi
        COMMAND ${MPIEXEC_EXECUTABLE} -n ${MPI_PROCESSES} $<TARGET_FILE:task1>
        DEPENDS task1
        WORKING_DIRECTORY ${CMAKE_RUNTIME_OUTPUT_DIRECTORY}
        COMMENT "Running task1 (e^x calculation) with ${MPI_PROCESSES} processes"
)

add_custom_target(run_task2_mpi
        COMMAND ${MPIEXEC_EXECUTABLE} -n ${MPI_PROCESSES} $<TARGET_FILE:task2>
        DEPENDS task2
        WORKING_DIRECTORY ${CMAKE_RUNTIME_OUTPUT_DIRECTORY}
        COMMENT "Running task2 (integral calculation) with ${MPI_PROCESSES} processes"
)
