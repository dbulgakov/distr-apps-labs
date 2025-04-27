## Building the Project

To compile the project, use the following command:

```bash
mpic++ -o mpi_integral main.cpp
```

To run the program with three processes

```bash
mpirun -np 3 ./mpi_integral
```