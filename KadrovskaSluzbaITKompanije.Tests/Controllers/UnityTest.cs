using KadrovskaSluzbaITKompanije.Controllers;
using KadrovskaSluzbaITKompanije.Models;
using KadrovskaSluzbaITKompanije.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace KadrovskaSluzbaITKompanije.Tests.Controllers
{
    [TestClass]
    class UnityTest
    {
        [TestMethod]
        public void GetReturnZaposleniWIthSameIdResultOk()
        {
            // Arrange
            var mockRepository = new Mock<IZaposleniRepository>();
            mockRepository.Setup(x => x.GetById(3)).Returns(new Zaposleni { Id = 3 });

            var controller = new ZaposleniController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(3);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Zaposleni>));
        }


        [TestMethod]
        public void PutZaposleniReturnBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IZaposleniRepository>();

            var controller = new ZaposleniController(mockRepository.Object);

            var zaposleni = new Zaposleni() { Id = 1, ImeIPrezime = "Milos Simic", GodinaRodjenja = 1998, GodinaZaposlenja = 2020, Rola = "Inzenjer", OrganizacionaJedinicaId = 3, Plata = 2500 };

            // Act
            IHttpActionResult actionResult = controller.PutZaposleni(30, zaposleni);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }


        [TestMethod]
        public void GetAllZaposleniReturnsMultipleObjects()
        {
            // Arrange
            List<Zaposleni> zaposleni = new List<Zaposleni>();
            zaposleni.Add(new Zaposleni { Id = 1, ImeIPrezime = "Milos Simic", Rola = "Direktor", GodinaZaposlenja = 2014, Plata = 2000 });
            zaposleni.Add(new Zaposleni { Id = 2, ImeIPrezime = "Marko Markovic", Rola = "Sekretar", GodinaZaposlenja = 2013, Plata = 2500 });

            var mockRepository = new Mock<IZaposleniRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(zaposleni.AsQueryable());
            var controller = new ZaposleniController(mockRepository.Object);

            // Act
            IQueryable<Zaposleni> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(zaposleni.Count, result.ToList().Count);
            Assert.AreEqual(zaposleni.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(zaposleni.ElementAt(1), result.ElementAt(1));
        }

        [TestMethod]
        public void PostPretragaReturnsMultipleObjects()
        {
            // Arrange
            List<Zaposleni> zaposleni = new List<Zaposleni>();
            zaposleni.Add(new Zaposleni { Id = 1, ImeIPrezime = "Milos Simic", Rola = "Direktor", GodinaZaposlenja = 2014, Plata = 2000 });
            zaposleni.Add(new Zaposleni { Id = 2, ImeIPrezime = "Marko Markovic", Rola = "Sekretar", GodinaZaposlenja = 2013, Plata = 2500 });
            zaposleni.Add(new Zaposleni { Id = 3, ImeIPrezime = "Zoran Zoranovic", Rola = "Sekretar", GodinaZaposlenja = 2012, Plata = 1400 });

            var pretragaModel = new ZaposleniPretraga() { Najmanje = 1500, Najvise = 3000 };
            var mockRepository = new Mock<IZaposleniRepository>();
            mockRepository.Setup(x => x.PostPretraga(pretragaModel)).Returns(zaposleni.AsQueryable());
            var controller = new ZaposleniController(mockRepository.Object);

            // Act
            IQueryable<Zaposleni> result = controller.Pretraga(pretragaModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(zaposleni.Count, result.ToList().Count);
            Assert.AreEqual(zaposleni.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(zaposleni.ElementAt(1), result.ElementAt(1));
            Assert.AreEqual(zaposleni.ElementAt(2), result.ElementAt(2));

        }

    }

}
