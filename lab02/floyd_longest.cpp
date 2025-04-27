#include <iostream>
#include <vector>
#include <iomanip>

constexpr int NEG_INF = -1e9;

void print_matrix(const std::vector<std::vector<int>>& dist) {
    int n = dist.size();
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (dist[i][j] <= NEG_INF / 2)
                std::cout << std::setw(7) << "-inf";
            else
                std::cout << std::setw(7) << dist[i][j];
        }
        std::cout << '\n';
    }
}

int main() {
    const int n = 6;
    std::vector<std::vector<int>> dist = {
        {0,    7,    NEG_INF, NEG_INF, 5,    NEG_INF},
        {-2,   0,    NEG_INF, NEG_INF, 1,    NEG_INF},
        {NEG_INF, 9, 0,    NEG_INF, NEG_INF, NEG_INF},
        {NEG_INF, NEG_INF, NEG_INF, 0,    NEG_INF, NEG_INF},
        {NEG_INF, NEG_INF, 4,    3,    0,    1},
        {NEG_INF, NEG_INF, NEG_INF, -3, NEG_INF, 0}
    };

    for (int k = 0; k < n; ++k)
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                if (dist[i][k] > NEG_INF && dist[k][j] > NEG_INF)
                    if (dist[i][j] < dist[i][k] + dist[k][j])
                        dist[i][j] = dist[i][k] + dist[k][j];

    std::cout << "Longest paths matrix:\n";
    print_matrix(dist);

    return 0;
}
