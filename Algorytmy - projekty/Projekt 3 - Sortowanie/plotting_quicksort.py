import matplotlib.pyplot as plt
import pandas as pd



if __name__ == '__main__':
    data = pd.read_csv("Interpretacja Wyników/pomiary_quicksort.csv")

    iterative_data = data[data["Typ"] == "iterative"]
    recurrent_data = data[data["Typ"] == "recurrent"]

    loc = "upper left"

    fig, ((ax1, ax2), (ax3, ax4), (ax5, ax6), (ax7, ax8)) = plt.subplots(4, 2)

    fig.text(0.5, 0.03, "Rozmiar Tablicy", ha="center")
    fig.text(0.03, 0.5, "Czas Sortowania (ms)", va="center",
             rotation="vertical")

    plt.rcParams["legend.borderpad"] = 0.2
    plt.rcParams["legend.markerscale"] = 0.7

    size_iter = iterative_data["Rozmiar"]
    time_iter = iterative_data["Czas"]

    size_recu = recurrent_data["Rozmiar"]
    time_recu = recurrent_data["Czas"]

    ax1.set_title("Iterative QuickSort")

    ax1.scatter(size_recu[recurrent_data["Pivot"] == "random"],
                time_recu[recurrent_data["Pivot"] == "random"],
                label = "Random", c = "b", s = 5)
    ax1.legend(loc = loc)

    ax3.scatter(size_recu[recurrent_data["Pivot"] == "left"],
                time_recu[recurrent_data["Pivot"] == "left"],
                label="First Element", c="g", s=5)
    ax3.legend(loc=loc)

    ax5.scatter(size_recu[recurrent_data["Pivot"] == "right"],
                time_recu[recurrent_data["Pivot"] == "right"],
                label="Last Element", c="r", s=5)
    ax5.legend(loc=loc)

    ax7.scatter(size_recu[recurrent_data["Pivot"] == "middle"],
                time_recu[recurrent_data["Pivot"] == "middle"],
                label="Median", c="m", s=5)
    ax7.legend(loc=loc)


    ax2.set_title("Recurrent QuickSort")

    ax2.scatter(size_iter[iterative_data["Pivot"] == "random"],
                time_iter[iterative_data["Pivot"] == "random"],
                label="Random", c="b", s=5)
    ax2.legend(loc=loc)

    ax4.scatter(size_iter[iterative_data["Pivot"] == "left"],
                time_iter[iterative_data["Pivot"] == "left"],
                label="First Element", c="g", s=5)
    ax4.legend(loc=loc)

    ax6.scatter(size_iter[iterative_data["Pivot"] == "right"],
                time_iter[iterative_data["Pivot"] == "right"],
                label="Last Element", c="r", s=5)
    ax6.legend(loc=loc)

    ax8.scatter(size_iter[iterative_data["Pivot"] == "middle"],
                time_iter[iterative_data["Pivot"] == "middle"],
                label="Median", c="m", s=5)
    ax8.legend(loc=loc)

    plt.savefig("Wykresy.png")
    plt.show()