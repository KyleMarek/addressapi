using System;
using System.Linq;

namespace MassAddress.Logic.Helpers
{
    public static class AddressHelper
    {
        private static readonly string[] States = { "AK ", " AL ", " AR ", " AZ ", " CA ", " CO ", " CT ", " DE ", " FL ", " GA ", " HI ", " IA ", " ID ", " IL ", " IN ", " KS ", " KY ", " LA ", " MA ", " MD ", " ME ", " MI ", " MN ", " MO ", " MS ", " MT ", " NC ", " ND ", " NE ", " NH ", " NJ ", " NM ", " NV ", " NY ", " OH ", " OK ", " OR ", " PA ", " RI ", " SC ", " SD ", " TN ", " TX ", " UT ", " VA ", " VT ", " WA ", " WI ", " WV ", " WY" };

        /// <summary>
        /// Function to find the State abbreviation in a string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string FindState(string address)
        {
            if (!string.IsNullOrEmpty(address) && States.Any(address.Contains)){
                return States.Where(address.Contains).FirstOrDefault().Trim();
            }
            return string.Empty;
        }
    }
}
