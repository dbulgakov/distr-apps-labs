#include <iostream>
#include <vector>
#include <iomanip>

constexpr int INF = 1e9;

void print_matrix(const std::vector<std::vector<int>>& dist) {
    int n = dist.size();
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (dist[i][j] >= INF / 2)
                std::cout << std::setw(7) << "inf";
            else
                std::cout << std::setw(7) << dist[i][j];
        }
        std::cout << '\n';
    }
}

int main() {
    const int n = 7;
    std::vector<std::vector<int>> dist = {
        {0,    1,   2,   7,   INF, 12,  INF},
        {INF,  0,  -1,   5,   INF, INF, INF},
        {INF, INF, 0,   3,   INF,  4,   6},
        {INF,  8,  -4,   0,   INF, INF, INF},
        {INF, 10, INF, 11,    0,   INF, 9},
        {INF, INF, 13, INF,  14,    0,  INF},
        {-5,  15, INF, -6,   INF, 16,    0}
    };

    for (int k = 0; k < n; ++k)
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                if (dist[i][k] < INF && dist[k][j] < INF)
                    if (dist[i][j] > dist[i][k] + dist[k][j])
                        dist[i][j] = dist[i][k] + dist[k][j];

    std::cout << "Shortest paths matrix:\n";
    print_matrix(dist);

    return 0;
}
