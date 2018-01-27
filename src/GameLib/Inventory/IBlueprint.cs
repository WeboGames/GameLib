using System.Collections.Generic;

namespace GameLib.Inventory
{
    public interface IBlueprint
    {
        IItemBundle Product 
        {
            get;
        }

        /// <summary>
        /// Get a list of the required items to craft this blueprint.
        /// </summary>
        /// <returns>Item list.</returns>
        List<IItemBundle> GetIngredients();

        /// <summary>
        /// Check if all required ingredients are present.
        /// </summary>
        /// <param name="ingredients">Used ingredients.</param>
        /// <returns>Whethere all ingredients are present.</returns>
        bool Match(List<IItemBundle> ingredients);

        /// <summary>
        /// Crafts an item if possible.
        /// </summary>
        /// <param name="ingredients">Ingredients to craft with.</param>
        /// <returns>Crafted item or null if not possible.</returns>
        IItemBundle Craft(List<IItemBundle> ingredients);
    }
}
