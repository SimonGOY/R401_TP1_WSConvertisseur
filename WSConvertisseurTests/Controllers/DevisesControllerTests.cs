using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        private DevisesController controller;
        [TestInitialize]
        public void InitTest()
        {
            // Arrange
            controller = new DevisesController();
        }

        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Act
            var result = controller.GetById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); //Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); //Test de la devise récupérée
        }

        [TestMethod]
        public void GetById_NotExistingIdPassed()
        {
            // Act
            var result = controller.GetById(-50);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas une NotFoundResult");
            Assert.IsNull(result.Value, "Value pas vide");
        }

        [TestMethod]
        public void GetAll()
        {
            // Act
            var result = controller.GetAll();
            List<Devise> devise = new List<Devise>();
            devise.Add(new Devise(1, "Dollar", 1.08));
            devise.Add(new Devise(2, "Franc Suisse", 1.07));
            devise.Add(new Devise(3, "Yen", 120));
            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Le retour n'est pas une liste");
            CollectionAssert.AreEqual(devise, result.ToList(), "Liste non égale");
        }

        [TestMethod]
        public void Post_AjoutCorrect()
        {
            // Act
            Devise newDevose = new Devise(4, "Rouble", 1510.6);
            var result = controller.Post(newDevose);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult");
            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            Assert.AreEqual(StatusCodes.Status201Created, routeResult.StatusCode, "Réponse pas de statut 201");
            Assert.AreEqual(newDevose, routeResult.Value);
        }

        /*[TestMethod]
        public void Post_AjoutIncorrect()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            Devise newDevose = new Devise(4, null, 1510.6);
            var result = controller.Post(newDevose);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult");
            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            Assert.AreEqual(StatusCodes.Status201Created, routeResult.StatusCode, "Réponse pas de statut 201");
            Assert.AreEqual(newDevose, routeResult.Value);
        }*/

        [TestMethod]
        public void Put_BadId()
        {
            // Act
            Devise newDevise = new Devise(1, "Dollar", 1.14);
            var result = controller.Put(2, newDevise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult");
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Pas un BadRequest");
        }

        [TestMethod]
        public void Put_InexistingId()
        {
            // Act
            Devise newDevise = new Devise(50, "Roubles", 1500.6);
            var result = controller.Put(50, newDevise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFound");
        }

        [TestMethod]
        public void Put_ValidUpdate()
        {
            // Act
            Devise newDevise = new Devise(2, "Roubles", 1500.6);
            var result = controller.Put(2, newDevise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Pas un NoContent");
        }

        [TestMethod]
        public void Delete_InexistingId()
        {
            // Act
            var result = controller.Delete(50);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFound");
        }

        [TestMethod]
        public void Delete_ValidId()
        {
            // Act
            var result = controller.Delete(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsNull(result.Result, "Il y a une erreur");
            Assert.IsNotNull(result.Value, "La devise retournée est null");
            Assert.AreEqual(1, result.Value.Id, "La devise retournée n'est pas la bonne");
        }
    }
}