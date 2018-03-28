using System;
using System.Collections.Generic;
using System.Linq;
using GameLib.Resources;

namespace GameLib.Inventory
{
    [Serializable]
    public class Blueprint : Serializable, IBlueprint
    {
        public List<Tuple<int, int>> Ingredients
        {
            get; set;
        }

        public Tuple<int, int> Product
        {
            get; set;
        }

        public Blueprint()
        {
            Ingredients = new List<Tuple<int, int>>();
        }

        public Blueprint(List<IItemBundle> ingredients, IItemBundle product)
        {
            Ingredients = new List<Tuple<int, int>>();
            foreach (var bundle in ingredients) {
                Ingredients.Add(new Tuple<int, int>(bundle.Preset.Id, bundle.Count));
            }
            Product = new Tuple<int, int>(product.Preset.Id, product.Count);
        }

        public Blueprint(List<Tuple<int, int>> ingredients, IItemBundle product)
        {
            Ingredients = ingredients;
            Product = new Tuple<int, int>(product.Preset.Id, product.Count);
        }

        public bool Match(List<Tuple<int, int>> ingredients)
        {
            var found = true;
            foreach (var ingredient in Ingredients) {
                var foundIngredient = ingredients.Where(s => s.Item1 == ingredient.Item1).ToList();
                found = foundIngredient != null ? true : false;
                if (!found) {
                    break;
                }
                var availableAmount = foundIngredient.AsEnumerable().Sum(s => s.Item2);
                if (availableAmount < ingredient.Item2) {
                    found = false;
                    break;
                }
            }
            return found;
        }

        public bool Match(List<IItemBundle> ingredients)
        {
            var found = new Dictionary<int, bool>();
            foreach (var ingredient in Ingredients) {
                var amount = 0;
                try {
                    amount = ingredients.Where(s => s.Preset != null && s.Preset.Id == ingredient.Item1).Sum(s => s.Count);
                }
                catch {
                    amount = 0;
                }
                found[ingredient.Item1] = amount >= ingredient.Item2;
            }
            var allFound = true;
            foreach (var singleFound in found) {
                allFound &= singleFound.Value;
            }
            return allFound;
        }

        public Tuple<int, int> Craft(List<Tuple<int, int>> ingredients)
        {
            return Match(ingredients) ? Clone(Product) : null;
        }

        public Tuple<int, int> Craft(List<IItemBundle> ingredients)
        {
            return Match(ingredients) ? Clone(Product) : null;
        }
    }
}