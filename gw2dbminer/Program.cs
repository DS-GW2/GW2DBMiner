using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using GW2Miner.Engine;
using GW2Miner.Domain;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2DBMiner
{
    class Program
    {
        static TradeWorker trader = new TradeWorker();

        //static string itemsFile = "DB\\items.json";
        //static string recipesFile = "DB\\recipes.json";
        static string itemsFile = "DB\\itemsapi.json";
        static string recipesFile = "DB\\recipesapi.json";

        static double CalculateProfit(int minCraftingCost, int itemId, out int breakEvenPrice, ref int quantity)
        {
            double profit = 0.0;
            List<ItemBuySellListingItem> offers = trader.get_buy_listings(itemId).Result;
            breakEvenPrice = 0;
            int quantitySold = 0;
            int leftToSell = quantity;

            if (offers != null && offers.Count > 0)
            {
                foreach (ItemBuySellListingItem offer in offers)
                {
                    if ((offer.PricePerUnit * 0.85) <= minCraftingCost || leftToSell <= offer.NumberAvailable)
                    {
                        if (leftToSell <= offer.NumberAvailable)
                        {
                            profit += (((offer.PricePerUnit * 0.85) - minCraftingCost) * leftToSell);
                            quantitySold += leftToSell;
                        }
                        breakEvenPrice = offer.PricePerUnit;
                        break;
                    }
                    profit += (((offer.PricePerUnit * 0.85) - minCraftingCost) * offer.NumberAvailable);
                    leftToSell -= offer.NumberAvailable;
                    quantitySold += offer.NumberAvailable;
                }
                quantity = quantitySold;
            }

            return profit;
        }

        static void Main(string[] args)
        {
            //try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, itemsFile)))
                {
                    //gw2dbItemParser itemParser = new gw2dbItemParser();
                    //List<gw2dbItem> itemList = itemParser.Parse(sr.BaseStream);

                    gw2apiItemParser itemParser = new gw2apiItemParser();
                    Dictionary<int, gw2apiItem> itemList = itemParser.Parse(sr.BaseStream);

                    //foreach (gw2dbItem item in trader.gw2dbItemList)
                    //foreach (gw2dbItem item in itemList)
                    foreach (gw2apiItem item in itemList.Values)
                    {
                        //if (item.SoldBy == null || item.SoldBy.Count == 0) continue;
                        //if (item.data_Id != 12669) continue; // Feast of Eggplant Fritters
                        //if (item.data_Id != 12323) continue; // Cherry Almond Bar
                        //if (item.data_Id != 12562) continue; // Tray[s] of Cherry Almond Bars
                        //if (item.data_Id != 36803) continue; // Apple Passion Fruit Pie[s]
                        //if (item.data_Id != 13686) continue; // Rampager's Destroyer Shield
                        //if (item.data_Id != 12153) continue; // Bag of Salt
                        //if (item.data_Id != 31037) continue; // Dreadwing of Hobbling
                        //if (item.data_Id != 31078) continue; // Foefire's Power of Peril
                        //if (item.data_Id != 31064) continue; // Firebringer of Smoldering
                        //if (item.data_Id != 31090) continue; // Mystic Wand
                        //if (item.data_Id != 24325) continue; // destroyer lodestone
                        //if (item.data_Id != 24310) continue; // onyx lodestone
                        //if (item.data_Id != 31059) continue; // Cragstone of Debility
                        //if (item.data_Id != 31037) continue; // Dreadwing of Hobbling
                        //if (item.data_Id != 31055) continue; // Titan's Vengance of Fire
                        //if (item.data_Id != 31067) continue; // Unspoken Curse of Blood
                        //if (item.data_Id != 31105) continue; // Jormag's Needle
                        //if (item.data_Id != 31055) continue; // Titans' Vengance
                        //if (item.data_Id != 31072) continue; // Winterbite
                        //if (item.data_Id != 31070) continue; // Wings of Dwayna
                        //if (item.data_Id != 12941) continue; // Ancient Longbow Stave
                        //if (item.data_Id != 19624) continue; // Gift of Ice
                        //if (item.data_Id != 41571) continue; // Bowl of Garlic Butter Sauce
                        //if (item.GW2DBExternalId != 67270) continue; // Almonds in Bulk
                        //if (item.data_Id != 24836) continue; // Superior Rune of the Scholar
                        //if (item.data_Id != 14577) continue; // Berserker's Pearl Handcannon
                        //if (item.data_Id != 13983) continue; // Berserker's Pearl Conch
                        //if (item.data_Id != 15433) continue; // Berserker's Pearl Reaver
                        //if (item.data_Id != 15356) continue; // Berserker's Pearl Bludgeoner
                        //if (item.data_Id != 10684) continue; // Berserker's Draconic Helm
                        //if (item.data_Id != 12474) continue; // Loaf of Saffron Bread
                        //if (item.data_Id != 12539) continue; // Bowl[s] of Mango Pie Filling
                        //if (item.Id != 12683) continue; // Tray[s] of Saffron Bread
                        //if (item.data_Id != 24801) continue; // Minor Rune of the Mesmer
                        //if (item.data_Id != 24816) continue; // Minor Rune of the Thief
                        //if (item.data_Id != 12565) continue; // Giant Chocolate Cherry Cake
                        //if (item.data_Id != 10902) continue; // Mighty Embroidered Wristguards
                        //if (item.data_Id != 24782) continue; // Superior Rune of the Pirate
                        //if (item.data_Id != 9129) continue; // Satchel of Valkyrie Prowler Armor
                        //if (item.data_Id != 12457) continue; // Omnomberry Pie
                        //if (item.data_Id != 24917) continue; // Embellished Ornate Ruby Jewel
                        //if (item.data_Id != 12589) continue; // Tray of Omnomberry Compote
                        //if (item.Id != 12591) continue; // Tray of Omnomberry Pie
                        //if (item.data_Id != 12565) continue; // Giant Chocolate Cherry Cake
                        //if (item.data_Id != 3948) continue; // Shadow Leggings
                        //if (item.data_Id != 3953) continue; // Shadow Helm
                        //if (item.data_Id != 10906) continue; // Precise Embroidered Wristguards
                        //if (item.Id != 24508) continue; // Ruby Orb
                        //if (item.data_Id != 10910) continue; // Mighty Embroidered Pants
                        //if (item.data_Id != 4242) continue; // Bloodsaw Leather Work Coat
                        //if (item.data_Id != 11508) continue; // Mighty Seeker Boots
                        //if (item.data_Id != 12498) continue; // Bowl of Peach Pie Filling
                        //if (item.data_Id != 12481) continue; // Bowl of Lemongrass Poultry Soup
                        //if (item.data_Id != 12580) continue; // Tray of Peach Tarts
                        //if (item.data_Id != 11670) continue; // Malign Seeker Pants
                        //if (item.data_Id != 12708) continue; // Pot of Fancy Creamy Mushroom Soup
                        //if (item.data_Id != 38206) continue; // Superior Rune of Altruism
                        //if (item.data_Id != 4233) continue; // Devout Legging
                        //if (item.data_Id != 10282) continue; // Mighty Chain Legs
                        //if (item.data_Id != 12444) continue; // Bowl of Poultry and Leek Soup
                        //if (item.data_Id != 12712) continue; // Pot of Poultry and Leek Soup
                        //if (item.data_Id != 30005) continue; // Charr Warhorn
                        //if (item.data_Id != 11048) continue; // Mighty Embroidered Mask
                        //if (item.data_Id != 10886) continue; // Mighty Embroidered Coat
                        //if (item.data_Id != 10894) continue; // Mighty Embroidered Sandles
                        //if (item.data_Id != 8700) continue; // Potion of Ascalonian Mages
                        //if (item.data_Id != 10264) continue; // Mighty Chain Coat
                        //if (item.data_Id != 4907) continue; // Duelist's Chain Gauntlets
                        //if (item.data_Id != 29997) continue; // Charr Scepter
                        //if (item.data_Id != 10270) continue; // Mighty Chain Boots
                        //if (item.data_Id != 10848) continue; // Hearty Gladiator Chestplate
                        //if (item.data_Id != 36053) continue; // Superior Sigil of the Night
                        //if (item.data_Id != 12551) continue; // Tray of Chocolate Bananas
                        //if (item.data_Id != 46739) continue; // Elonian Leather Squares
                        //if (item.data_Id != 10764) continue; // Hearty Gladiator Pauldrons
                        //if (item.data_Id != 12464) continue; // Rare Veggie Pizza
                        //if (item.data_Id != 4230) continue; // Devout Shoes
                        //if (item.data_Id != 46707) continue; // Mathilde's Dire Inscription
                        //if (item.data_Id != 31102) continue; // Mystic Trident
                        //if (item.Id != 36072) continue; // Gift of Souls
                        //if (item.data_Id != 12758) continue; // Unidentified Gray Dye
                        //if (item.data_Id != 48915) continue; // Toxic Sharpening Stone
                        //if (item.data_Id != 46736) continue; // Spiritwood Plank
                        //if (item.data_Id != 46738) continue; // Deldrimor Steel Ingot
                        //if (item.data_Id != 48911) continue; // Superior Sigil of Torment
                        //if (item.data_Id != 48916) continue; // Toxic Maintenance Oil
                        //if (item.data_Id != 48921) continue; // Bowl of Marjory's Experimental Chili
                        //if (item.data_Id != 48907) continue; // Superior Rune of Antitoxin
                        //if (item.data_Id != 46695) continue; // Zojia's Berserker Insignia
                        //if (item.Id != 46741) continue; // Bolt of Damask
                        //if (item.Id != 47920) continue; // Beigarth's Doublet
                        //if (item.Id != 47917) continue; // Beigarth's Breeches
                        //if (item.Id != 45874) continue; // Deldrimor Steel Helmet Lining
                        //if (item.Id != 36070) continue; // The Crossing
                        //if (item.Id != 12523) continue; // Bowl of Orange Coconut Frosting
                        //if (item.Id != 49457) continue; // Superior Sigil of Momentum
                        //if (item.Id != 48699) continue; // Rabid Emblazoned Helm
                        //if (item.Id != 19743) continue; // Linen Scrap
                        //if (item.Id != 12628) continue; // Feast of Tarragon Stuffed Poultrys
                        //if (item.Id != 12671) continue; // Tray of Tarragon Bread
                        if (item.Id != 46703) continue; // Grizzlemouth's Rabid Inscription
                        //if (item.Id != 46704) continue; // Hronk;s Magi Inscription
                        //if (item.Id != 46705) continue; // Chorben's Soldier Inscription

                        //Console.WriteLine("{0} {1} {2}", item.Name, item.Id, item.data_Id);
                        //Console.WriteLine("TypeId: {0}", item.GW2DBTypeId.ToString());

                        Console.WriteLine("{0} {1}", item.Name, item.Id);
                        Console.WriteLine("TypeId: {0}", item.TypeId.ToString());

                        switch (item.TypeId)
                        {
                            case TypeEnum.Armor:
                                Console.WriteLine("Armor Type: {0}", item.Armor.SubTypeId.ToString());
                                Console.WriteLine("Armor Weight Type: {0}", item.Armor.ArmorWeightType.ToString());
                                break;

                            case TypeEnum.Weapon:
                                Console.WriteLine("Weapon Type: {0}", item.Weapon.SubTypeId.ToString());
                                break;
                        }

                        Console.WriteLine("Description: {0}", item.Description);
                        Console.WriteLine("Level: {0}", item.MinLevel);
                        Console.WriteLine("Required Level: {0}", item.MinLevel);
                        Console.WriteLine("Rarity: {0}", item.RarityId.ToString());

                        //foreach (gw2dbSoldBy npc in item.SoldBy)
                        //{
                        //    Console.WriteLine("NPC: {0}", npc.NPCExternalID);
                        //    //if (npc.KarmaCost == 0 && npc.GoldCost == 0) Debugger.Break();
                        //    Console.WriteLine("Karma Cost: {0}", npc.KarmaCost);
                        //    Console.WriteLine("Skill Points Cost: {0}", npc.SkillPointCost);
                        //    Console.WriteLine("Gold Cost: {0}", npc.GoldCost);
                        //}

                        trader.UseGW2SpidyForCraftingCost = false;

                        if (trader.GW2APILoaded)
                        {
                            //gw2dbItem dbItem = trader.GetGW2DBItem(item.data_Id);
                            //gw2apiItem dbItem = trader.GetGW2APIItem(item.data_Id);
                            gw2apiItem dbItem = trader.GetGW2APIItem(item.Id);
                            RecipeCraftingCost recipeCraftingCost = trader.MinCraftingCost(dbItem.Recipes);
                            Console.WriteLine("Crafting Cost: {0}", recipeCraftingCost == null ? 0 : recipeCraftingCost.GoldCost);
                            Console.WriteLine("               {0} Karma", recipeCraftingCost == null ? 0 : recipeCraftingCost.KarmaCost);
                            Console.WriteLine("               {0} Skill Point[s]", recipeCraftingCost == null ? 0 : recipeCraftingCost.SkillPointsCost);
                            //Console.WriteLine("Cost: {0}", trader.MinAcquisitionCost(dbItem.Recipe));
                            foreach (gw2apiRecipe recipe in dbItem.Recipes)
                            {
                                trader.MinAcquisitionCost(recipe);
                                recipe.Print("");
                                Console.WriteLine(" ");
                                recipe.ShoppingList(false);
                                Console.WriteLine(" ");
                                //break;
                            }

                            int maxOfferUnitPrice = dbItem.Recipes[0].CreatedItemMaxBuyUnitPrice;
                            int breakEvenPrice, quantity = dbItem.Recipes[0].CreatedItemAvailability;
                            Console.WriteLine("Profit: {0} Sale: {1} BreakEven: {2} Quantity: {3}", CalculateProfit(recipeCraftingCost.GoldCost, dbItem.Id, out breakEvenPrice, ref quantity),
                                                maxOfferUnitPrice, breakEvenPrice, quantity);
                            //dbItem.Recipes[1].Print("");
                        }
                    }
                    //using (var jsonTextReader = new JsonTextReader(sr))
                    //{
                    //    JToken token = JObject.ReadFrom(jsonTextReader);
                    //    for (int i = 0; i < token.Count(); i++)
                    //    {
                    //        //if (token[i]["RequiresRecipeItem"].ToObject<bool>() == false) continue;
                    //        //if (token[i]["DataID"].ToObject<int>() != 12155) continue;
                    //        //if (token[i]["ExternalID"].ToObject<int>() != 25625) continue;
                    //        //if (token[i]["SoldBy"].HasValues == false) continue;
                    //        //if (token[i]["ArmorWeightType"] == null || token[i]["ArmorWeightType"].ToObject<int>() != 4) continue;
                    //        //if (token[i]["Type"].ToObject<int>() != 2) continue;
                    //        foreach (JToken o in token[i].Children())
                    //        {
                    //            if (o is JProperty)
                    //            {
                    //                var prop = o as JProperty;
                    //                Console.WriteLine("{0}={1}", prop.Name, prop.Value);
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            //catch (Exception e)
            //{
            //    // handle error
            //    Console.WriteLine("The file could not be read:");
            //    Console.WriteLine(e.Message);
            //}

            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }
    }
}
