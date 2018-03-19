using System;
using System.Collections.Generic;

namespace GameLib.Inventory
{
    public interface IBlueprint
    {
        Tuple<int, int> Product 
        {
            get; set;
        }

        /// <summary>
        /// Get a list of the required items to craft this blueprint.
        /// </summary>
        List<Tuple<int, int>> Ingredients
        {
            get; set;
        }

        /// <summary>
        /// Check if all required ingredients are present.
        /// </summary>
        /// <param name="ingredients">Used ingredients.</param>
        /// <returns>Whethere all ingredients are present.</returns>
        bool Match(List<Tuple<int, int>> ingredients);
        bool Match(List<IItemBundle> ingredients);

        /// <summary>
        /// Crafts an item if possible.
        /// </summary>
        /// <param name="ingredients">Ingredients to craft with.</param>
        /// <returns>Crafted item or null if not possible.</returns>
        Tuple<int, int> Craft(List<Tuple<int, int>> ingredients);
        Tuple<int, int> Craft(List<IItemBundle> ingredients);
    }
}
