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

        static string itemsFile = "DB\\items.json";
        static string recipesFile = "DB\\recipes.json";

        static void Main(string[] args)
        {
            //try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, itemsFile)))
                {
                    gw2dbItemParser itemParser = new gw2dbItemParser();
                    List<gw2dbItem> itemList = itemParser.Parse(sr.BaseStream);

                    //foreach (gw2dbItem item in trader.gw2dbItemList)
                    foreach (gw2dbItem item in itemList)
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
                        if (item.data_Id != 24836) continue; // Superior Rune of the Scholar

                        Console.WriteLine("{0} {1} {2}", item.Name, item.Id, item.data_Id);
                        Console.WriteLine("TypeId: {0}", item.GW2DBTypeId.ToString());

                        switch (item.GW2DBTypeId)
                        {
                            case GW2DBTypeEnum.Armor:
                                Console.WriteLine("Armor Type: {0}", item.ArmorType.ToString());
                                Console.WriteLine("Armor Weight Type: {0}", item.ArmorWeightType.ToString());
                                break;

                            case GW2DBTypeEnum.Weapon:
                                Console.WriteLine("Weapon Type: {0}", item.WeaponType.ToString());
                                break;
                        }

                        Console.WriteLine("Description: {0}", item.Description);
                        Console.WriteLine("Level: {0}", item.Level);
                        Console.WriteLine("Required Level: {0}", item.MinLevel);
                        Console.WriteLine("Rarity: {0}", item.GW2DBRarityId.ToString());
                        Console.WriteLine("Value: {0}", item.Value);
                        Console.WriteLine("Defense: {0}", item.Defense);
                        Console.WriteLine("Min Power: {0}", item.MinPower);
                        Console.WriteLine("Max Power: {0}", item.MaxPower);

                        foreach (gw2dbSoldBy npc in item.SoldBy)
                        {
                            Console.WriteLine("NPC: {0}", npc.NPCExternalID);
                            //if (npc.KarmaCost == 0 && npc.GoldCost == 0) Debugger.Break();
                            Console.WriteLine("Karma Cost: {0}", npc.KarmaCost);
                            Console.WriteLine("Skill Points Cost: {0}", npc.SkillPointCost);
                            Console.WriteLine("Gold Cost: {0}", npc.GoldCost);
                        }

                        if (trader.GW2DBLoaded)
                        {
                            gw2dbItem dbItem = trader.GetGW2DBItem(item.data_Id);
                            RecipeCraftingCost recipeCraftingCost = trader.MinCraftingCost(dbItem.Recipes);
                            Console.WriteLine("Crafting Cost: {0}", recipeCraftingCost == null ? 0 : recipeCraftingCost.GoldCost);
                            Console.WriteLine("               {0} Karma", recipeCraftingCost == null ? 0 : recipeCraftingCost.KarmaCost);
                            Console.WriteLine("               {0} Skill Point[s]", recipeCraftingCost == null ? 0 : recipeCraftingCost.SkillPointsCost);
                            //Console.WriteLine("Cost: {0}", trader.MinAcquisitionCost(dbItem.Recipe));
                            foreach (gw2dbRecipe recipe in dbItem.Recipes)
                            {
                                trader.MinAcquisitionCost(recipe);
                                recipe.Print("");
                                Console.WriteLine(" ");
                                //break;
                            }
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
