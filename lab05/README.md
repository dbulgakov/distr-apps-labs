To compile the project, use the following command:

```bash
mpic++ -o mpi_floyd mpi_floyd.cpp
```

To run the program with three processes

```bash
mpirun -np 3 ./mpi_floyd
```