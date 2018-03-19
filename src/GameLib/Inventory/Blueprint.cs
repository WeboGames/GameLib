using System;
using System.Collections.Generic;
using System.Linq;
using GameLib.Resources;

namespace GameLib.Inventory
{
    [Serializable]
    public class Blueprint : Serializable, IBlueprint
    {
        public List<Tuple<int, int>> Ingredients { get; set; }

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
                var foundIngredient = ingredients.SingleOrDefault(s => s.Item2 >= ingredient.Item2 
                                                            && s.Item1 == ingredient.Item1);
                found = foundIngredient != null ? true : false;
                if (!found) {
                    break;
                }
            }
            return found;
        }

        public bool Match(List<IItemBundle> ingredients)
        {
            var found = true;
            foreach (var ingredient in Ingredients) {
                var foundIngredient = ingredients.SingleOrDefault(s => s.Count >= ingredient.Item2 
                                                            && s.Preset.Id == ingredient.Item1);
                found = foundIngredient != null ? true : false;
                if (!found) {
                    break;
                }
            }
            return found;
        }

        public Tuple<int, int> Craft(List<Tuple<int, int>> ingredients)
        {
            return Match(ingredients) ? Product : null;
        }

        public Tuple<int, int> Craft(List<IItemBundle> ingredients)
        {
            return Match(ingredients) ? Product : null;
        }
    }
}
