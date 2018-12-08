using HalalFramework.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Solver
{
    /*
        Evolúciós algoritmusok 
        Alapjai biológiai eredetüek. 
        Fontos tulajdonsága, hogy nem problémaspecifikus megoldás. 
        MUTÁCIÓ, KERESZTEZÉS, TERMÉSZETES KIVÁLASZTÓDÁS
        Több iteráción keresztül finomítja a megoldást.

        Az S a lehetséges DNS variációk -> Városok listája (genom)
        DNS elemei pedig az egyes DNS-ek -> Egy város adatai (genotype) 
        Majd ezekből születik a megoldási lehetőség (phenotype)
    */
    class TravellingSalesmanGeneticAlgo
    {
        List<Town> dna;
        TravellingSalesman problem;
        public TravellingSalesmanGeneticAlgo(string filename)
        {
            problem = new TravellingSalesman(filename);
            //A genetikus algo bit stringben tárolja a DNS-t, 
            //Ebben a megvalósításban a DNS a városok listája.
            //Jobban hasonlít így a Evolution Strategies-hez, ami valós vektorokban tárolja a DNS-t
            dna = problem.Towns;

            GA(dna, problem.objective, true);
        }

        private void GA(List<Route> routes, object objective, bool v)
        {
            //P <- InitializePopulation(S)
            //Evaluation(P, f) --fitness hozzárendelés
            //pbest <-min(f(x))- {bármely x E P}
            var P = routes;


        }

        private void GA(List<Town> towns, Func<List<Town>, double> objective, bool v)
        {


        }

    }
}
