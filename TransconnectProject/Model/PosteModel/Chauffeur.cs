﻿using TransconnectProject.Model.DepartementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransconnectProject.Model.CommandeModel;
using System.Runtime.Serialization;

namespace TransconnectProject.Model.PosteModel
{
    public class Chauffeur : Poste
    {
        private List<Commande> listeDeCommandes;
        public Chauffeur() : base("Chauffeur", new DepDesOps(),1600)
        {
            this.listeDeCommandes = new List<Commande>();
        }
        public List<Commande> ListeDeCommandes { get => this.listeDeCommandes; set => this.listeDeCommandes = value; }
        public void addCommande(Commande c)
        {
            var exist = this.listeDeCommandes.Find(x => x.DateDeLivraison.Day == c.DateDeLivraison.Day && x.DateDeLivraison.Year==c.DateDeLivraison.Year && x.DateDeLivraison.Month == c.DateDeLivraison.Month);
            if (exist == null)
            {
                this.listeDeCommandes.Add(c);
                Console.WriteLine("\nCommande ajoute à la liste des commandes du chauffeur !\n");
            }
            else throw new Exception("Erreur, le chauffeur ne peux effectuer cette livraison à ce jour: " + c.DateDeLivraison.ToString("d"));
        }
        public static int getTarif() { return 60; }//pour une livraison
    }
}
