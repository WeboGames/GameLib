using System.Collections.Generic;
using System.Linq;

namespace GameLib.Inventory
{
    public class Blueprint : IBlueprint
    {
        private readonly List<IItemBundle> _ingredients;

        public Blueprint(List<IItemBundle> ingredients, IItemBundle product)
        {
            _ingredients = ingredients;
            Product = product;
        }

        public IItemBundle Product
        {
            get;
        }

        public List<IItemBundle> GetIngredients()
        {
            return _ingredients;
        }

        public bool Match(List<IItemBundle> ingredients)
        {
            var found = true;
            foreach (var ingredient in _ingredients) {
                var foundIngredient = ingredients.SingleOrDefault(s => s.Count >= ingredient.Count 
                                                                  && s.Id == ingredient.Id);
                found = foundIngredient != null ? true : false;
                if (!found) {
                    break;
                }
            }
            return found;
        }

        public IItemBundle Craft(List<IItemBundle> ingredients)
        {
            return Match(ingredients) ? Product : null;
        }
    }
}
