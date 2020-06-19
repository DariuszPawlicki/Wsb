import matplotlib.pyplot as plt
import pandas as pd


if __name__ == "__main__":
    data = pd.read_csv("pomiary.csv")

    font_size = 8

    results_by_methods = {}


    for method in data["Sortowanie"].unique():
        results_by_methods[method] = data[data["Sortowanie"] == method]
        results_by_methods[method] = results_by_methods[method].drop(["Sortowanie"], axis = 1)


    for method, data in results_by_methods.items():
        plt.figure(figsize=(30, 30))

        plt.rcParams["xtick.labelsize"] = 6
        plt.rcParams["ytick.labelsize"] = 6
        plt.rcParams["legend.fontsize"] = 7
        plt.rcParams["legend.borderpad"] = 0.5
        plt.rcParams["legend.markerscale"] = 0.7

        fig, ((ax1, ax2), (ax3, ax4), (ax5, _)) = plt.subplots(3, 2)
        _.set_visible(False)

        fig.text(0.5, 0.03, "Rozmiar Tablicy", ha = "center")
        fig.text(0.03, 0.5, "Czas Sortowania (ms)", va = "center",
                 rotation = "vertical")

        fig.suptitle(method)

        size = results_by_methods[method]["RozmiarTablicy"]
        init_type = results_by_methods[method]["MetodaInicjalizacji"]
        time = results_by_methods[method]["CzasSortowania"]

        ax1.scatter(size.where(init_type == "random"),
                    time.where(init_type == "random"),
                    label = "Random", s = 8, c = "b")
        ax1.legend()

        ax2.scatter(size.where(init_type == "ascending"),
                    time.where(init_type == "ascending"),
                    label="Ascending", s=8, c = "g")
        ax2.legend()

        ax3.scatter(size.where(init_type == "descending"),
                    time.where(init_type == "descending"),
                    label="Descending", s=8, c = "r")
        ax3.legend()

        ax4.scatter(size.where(init_type == "constant"),
                    time.where(init_type == "constant"),
                    label="Constant", s=8, c = "m")
        ax4.legend()

        ax5.scatter(size.where(init_type == "v-shape"),
                    time.where(init_type == "v-shape"),
                    label="V-Shape", s=8, c = "y")
        ax5.legend()

        plt.savefig("Wykresy/{}.png".format(method))


