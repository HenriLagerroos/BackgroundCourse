
using System;
using System.Collections.Generic;

public class PlayerCreator
{        public void MakeMillion(){
                        
                List<Player> players = new List<Player>();

                while(players.Count < 1000001)
                {
                    Player player = new Player();
                    players.Add(player);
                    bool unique = false;
                    Guid id;

                    do{
                        id = Guid.NewGuid();
                        for(int i = 0; i < players.Count; i++)
                        {
                            if(players[i].Id == id)
                            {
                                unique = false;
                            } else if (i == players.Count - 1)
                            {
                            unique = true;     
                            }
                        }
                    }while (!unique);

                    player.Id = id;
                    
                    Console.WriteLine(id);
                }
        }

        public void MakeSome()
        {
            Player player = new Player();
            List<Item> items = new List<Item>();
            player.Items = items;
            Item i1 = new Item();
            i1.Level = 1;
            player.Items.Add(i1);

            Item i2 = new Item();
            i2.Level = 2;
            player.Items.Add(i2);

            Item i3 = new Item();
            i3.Level = 3;
            player.Items.Add(i3);

            Item best = player.GetHighestLevelItem();
            Console.WriteLine(best.Level);
        }

        public void MakeSomeThing()
        {
            Player player = new Player();
            List<Item> items = new List<Item>();
            player.Items = items;
            Item i1 = new Item();
            i1.Level = 1;
            player.Items.Add(i1);

            Item i2 = new Item();
            i2.Level = 2;
            player.Items.Add(i2);

            Item i3 = new Item();
            i3.Level = 3;
            player.Items.Add(i3);

            Item[] a1 = player.GetItems();
            Console.WriteLine(a1[1].Level);

            Item[] a2 = player.GetItemsLinq();
            Console.WriteLine(a2[2].Level);
        }

        public void MakeFun()
        {
            Player player = new Player();
            List<Item> items = new List<Item>();
            player.Items = items;
            Item i1 = new Item();
            i1.Level = 1;
            player.Items.Add(i1);

            Item i2 = new Item();
            i2.Level = 2;
            player.Items.Add(i2);

            Item i3 = new Item();
            i3.Level = 3;
            player.Items.Add(i3);

            Item a1 = player.GetFirst();
            Console.WriteLine(a1.Level);

            Item a2 = player.GetFirstLinq();
            Console.WriteLine(a2.Level);
        }

        public void MakeDelegate()
        {
            Player player = new Player();
            List<Item> items = new List<Item>();
            player.Items = items;
            Item i1 = new Item();
            i1.Level = 1;
            i1.Id = Guid.NewGuid();
            player.Items.Add(i1);

            Item i2 = new Item();
            i2.Level = 2;
            i2.Id = Guid.NewGuid();
            player.Items.Add(i2);

            Item i3 = new Item();
            i3.Level = 3;
            i3.Id = Guid.NewGuid();
            player.Items.Add(i3);

            Extensions.ProcessEachItem(player, Extensions.PrintItem);
        }

        public void MakeLambda()
        {
            Player player = new Player();
            List<Item> items = new List<Item>();
            player.Items = items;
            Item i1 = new Item();
            i1.Level = 1;
            i1.Id = Guid.NewGuid();
            player.Items.Add(i1);

            Item i2 = new Item();
            i2.Level = 2;
            i2.Id = Guid.NewGuid();
            player.Items.Add(i2);

            Item i3 = new Item();
            i3.Level = 3;
            i3.Id = Guid.NewGuid();
            player.Items.Add(i3);

            Extensions.ProcessEachItem(player, item => { Console.WriteLine(item.Id + ", " + item.Level); });
        }

        public void MakeGamePlayer()
        {
                List<Player> players = new List<Player>();

                while(players.Count < 101)
                {
                    Player player = new Player();
                    players.Add(player);
                    bool unique = false;
                    Guid id;

                    do{
                        id = Guid.NewGuid();
                        for(int i = 0; i < players.Count; i++)
                        {
                            if(players[i].Id == id)
                            {
                                unique = false;
                            } else if (i == players.Count - 1)
                            {
                            unique = true;     
                            }
                        }
                    }while (!unique);

                    player.Id = id;
                    player.Score = players.Count * 10;
                }

                Game<Player> game1 = new Game<Player>(players);

                Player[] topPlayers = game1.GetTop10Players();

            foreach(Player p in topPlayers)
                Console.WriteLine(p.Id + " " + p.Score);   
        }

        public void MakeGamePlayerForAnotherGame()
        {
                List<PlayerForAnotherGame> players = new List<PlayerForAnotherGame>();

                while(players.Count < 101)
                {
                    PlayerForAnotherGame player = new PlayerForAnotherGame();
                    players.Add(player);
                    bool unique = false;
                    Guid id;

                    do{
                        id = Guid.NewGuid();
                        for(int i = 0; i < players.Count; i++)
                        {
                            if(players[i].Id == id)
                            {
                                unique = false;
                            } else if (i == players.Count - 1)
                            {
                            unique = true;     
                            }
                        }
                    }while (!unique);

                    player.Id = id;
                    player.Score = players.Count * 20;
                }

                Game<PlayerForAnotherGame> game1 = new Game<PlayerForAnotherGame>(players);

            PlayerForAnotherGame[] topPlayers = game1.GetTop10Players();

            foreach(PlayerForAnotherGame p in topPlayers)
                Console.WriteLine(p.Id + " " + p.Score);              
        }
}