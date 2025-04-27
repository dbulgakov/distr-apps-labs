To compile the project, use the following command:

```bash
mpic++ -o mpi_exchange main.cpp
```

To run the program with three processes

```bash
mpirun -np 3 ./mpi_exchange
```