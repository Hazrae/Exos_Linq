using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LINQDataContext;

namespace ExerciceDeLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext dc = new DataContext();

            #region Exo 2.1
            {

                var queryResult = from e in dc.Students

                                  select new
                                  {
                                      nom = e.First_Name,
                                      dateDeNaissance = e.BirthDate,
                                      login = e.Login,
                                      result = e.Year_Result
                                  };

                foreach (var e in queryResult)
                {
                    Console.WriteLine($"{e.nom} né le {e.dateDeNaissance} et dont le login est " +
                                      $"{e.login} a eu {e.result}");
                }
            }
            #endregion

            #region Exo 2.2
            {

                //Exercice 2.2    Ecrire une requête pour présenter, pour chaque étudiant, 
                //son nom complet(nom et prénom séparés par un espace), 
                //son id et sa date de naissance.
                var queryResult = dc.Students.Select(s => new
                {
                    nom = s.First_Name + " " + s.Last_Name,
                    id = s.Student_ID,
                    dateDeNaissance = s.BirthDate
                });

                foreach (var s in queryResult)
                {
                    Console.WriteLine($"{s.nom} dont l'ID est {s.id} est né le {s.dateDeNaissance}");
                }
            }
            #endregion

            #region Exo 2.3
            {
                //Ecrire une requête pour présenter, pour chaque étudiant, dans une seule 
                //chaine de caractère l’ensemble des données relatives à un étudiant 
                //séparées par le symbole |.

                var queryResult = dc.Students.Select(s => new
                {
                    Stud = s.Student_ID + " | " +
                           s.First_Name + " | " +
                           s.Last_Name + " | " +
                           s.Login+ " | " +
                           s.BirthDate + " | " +
                           s.Course_ID + " | " +
                           s.Section_ID + " | " 
                });

                foreach(var s in queryResult)
                {
                    Console.WriteLine(s.Stud);
                }

            }
            #endregion

            #region Exo 3.1
            {
                //Pour chaque étudiant né avant 1955, donner le nom, le résultat annuel et le statut. Le statut prend 
                //la valeur « OK » si l’étudiant à obtenu au moins 12 comme résultat annuel et « KO » dans le cas contraire. 

                var queryResult = from c in dc.Students
                                  where c.BirthDate.Year < 1955
                                  select new
                                  {
                                      nom = c.Last_Name,
                                      result = c.Year_Result,
                                      statut = (c.Year_Result > 12) ? "OK" : "KO"
                                  };
                foreach (var s in queryResult)
                {
                    Console.WriteLine($"{s.nom} -- {s.result} -- {s.statut}");
                }
            }
            #endregion

            #region Exo 3.2
            {
                //Donner pour chaque étudiant entre 1955 et 1965 le nom, le résultat annuel et la catégorie
                //à laquelle il appartient.La catégorie est fonction du résultat annuel obtenu; un résultat inférieur 
                //à 10 appartient à la catégorie « inférieure », un résultat égal
                //à 10 appartient à la catégorie « neutre », un résultat autre appartient à la catégorie « supérieure ».

                IEnumerable<Student> queryStudent = dc.Students.Where(s => s.BirthDate.Year <= 1965
                                                                        && s.BirthDate.Year >= 1955);

                var queryStud = queryStudent.Select(s => new
                { 
                    nom = s.First_Name,
                    cat = (s.Year_Result == 10 ? "neutre" : s.Year_Result < 10 ? "inférieur" : "supérieure")
                }
                );

                foreach (var s in queryStud)
                {
                    Console.WriteLine($"{s.nom} {s.cat}");
                }

            }
            #endregion

            #region Exo 3.3
            {

                //Exercice 3.3	Ecrire une requête pour présenter le nom, l’id de section et de tous 
                //les étudiants qui ont un nom de famille qui termine par r.

                var queryResult = dc.Students.Where(s => s.Last_Name.ToLower().EndsWith("r")).Select(s=>new {Last_Name = s.Last_Name, Section_ID = s.Section_ID});

                foreach (var s in queryResult)
                {
                    Console.WriteLine(s.Last_Name + " " + s.Section_ID);
                }
            }
            #endregion

            #region Exo 3.4
            {

               //Ecrire une requête pour présenter le nom et le résultat annuel classé par résultats 
               //annuels décroissant de tous les étudiants qui ont obtenu un résultat annuel inférieur ou égal à 3.

                IEnumerable <Student> enumStu = dc.Students.Where(c => c.Year_Result <= 3);
                var query = enumStu.Select(c => new
                {
                    nom = c.Last_Name,
                    result = c.Year_Result
                }).OrderByDescending(c => c.result);

                foreach (var s in query)
                {
                    Console.WriteLine($"{s.nom} --- {s.result}");
                }
            }
            #endregion

            #region 3.5
            {

                //Ecrire une requête pour présenter le nom complet(nom et prénom séparés par un espace) et le
                //résultat annuel classé par nom croissant sur le nom de tous les étudiants appartenant
                //à la section 1110.

                var queryResult = dc.Students.Where(c => c.Section_ID == 1110).Select(c => new
                {
                    nom = c.Last_Name + " " + c.First_Name,
                    result = c.Year_Result
                }).OrderBy(c => c.nom);

                foreach (var s in queryResult)
                {
                    Console.WriteLine($"{s.nom} --- {s.result}");

                }
            }
            #endregion

            #region 3.6
            {

                //Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel classé par ordre
                //croissant sur la section de tous les étudiants appartenant aux sections 1010 et 1020 ayant un résultat 
                //annuel qui n’est pas compris entre 12 et 18.

                var queryResult = from c in dc.Students
                                  where (c.Year_Result > 12 && c.Year_Result < 18) && (c.Section_ID == 1010 || c.Section_ID == 1020)
                                  orderby c.Section_ID
                                  select new
                                  {
                                      nom = c.Last_Name,
                                      sec = c.Section_ID,
                                      result = c.Year_Result
                                  };

                foreach (var s in queryResult)
                {
                    Console.WriteLine($"{s.nom} -- {s.sec} -- {s.result}");
                }
            }
            #endregion

            #region 3.7
            {

                //Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel sur 100
                //(nommer la colonne ‘result_100’) classé par ordre décroissant du résultat de tous les étudiants
                //appartenant aux sections commençant par 13 et ayant un résultat annuel sur 100 inférieur 
                //ou égal à 60.

                //méthodes d'extension

                var queryResult = dc.Students.Where(c => c.Section_ID.ToString().StartsWith("13") && c.Year_Result <= 6).Select(c => new
                {
                    nom = c.Last_Name,
                    section = c.Section_ID,
                    result_100 = c.Year_Result * 10
                }).OrderByDescending(c => c.result_100);


                // opérateurs 

                var queryResult2 = from c in dc.Students
                                   orderby c.Year_Result descending
                                   where (c.Section_ID).ToString().StartsWith("13") && c.Year_Result <= 6
                                   select new
                                   {
                                       nom = c.Last_Name,
                                       section = c.Section_ID,
                                       result_100 = c.Year_Result * 10
                                   };
                                   
                // affichage collection faite avec méthodes d'extension

                foreach (var c in queryResult)
                {
                    Console.WriteLine($"{c.nom} -- {c.section} -- {c.result_100}");
                }

                // affichage collection faite avec les opérateurs

                foreach (var c in queryResult2)
                {
                    Console.WriteLine($"{c.nom} -- {c.section} -- {c.result_100}");
                }


            }
            #endregion



            #region Console.ReadLine()
            Console.ReadLine();
            #endregion
        }
    }
}
