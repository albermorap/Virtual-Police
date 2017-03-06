using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;



namespace VP_TestsFuncionales
{
    /// <summary>
    /// Descripción resumida de CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        static ApplicationUnderTest app;
        static string sut;
        public CodedUITest1()
        {

        }


     /**   [TestMethod]
        public void BufferOverflowUsuario() 
        {
            this.UIMap.BufferOverflowUsuarioLogin();
            Assert.Fail("Desbordamiento de buffer");
            //No se puede implementar el Assert hasta que el error haya sido corregido
        }

        [TestMethod]
        public void BufferOverflowPassword()
        {
            this.UIMap.BufferOverflowPasswordLogin();
            Assert.Fail("Desbordamiento de buffer");
            //No se puede implementar el Assert hasta que el error haya sido corregido
        }**/

        [TestMethod]
        public void Login()
        {
            this.UIMap.PruebaGrabada();
            this.UIMap.AssertMethodPrueba();
        }

        [TestMethod]
        public void RealizarAltaAgente()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.FB_AltaAgente();
            this.UIMap.AssertMethodAltaAgente();
        }

        /**[TestMethod]
        public void BufferOverflowDNI()
        {
            this.UIMap.BufferOverflowDniAltaAgente();
            Assert.Fail("Desbordamiento de buffer");
            //No se puede implementar el Assert hasta que el error haya sido corregido
        }

        [TestMethod]
        public void BufferOverflowNumPlacaAgente()
        {
            this.UIMap.BufferOverflowNumPlacaAltaAgente(); 
            Assert.Fail("Desbordamiento de buffer");
            //No se puede implementar el Assert hasta que el error haya sido corregido
        }

        [TestMethod]
        public void BufferOverflowEmailAgente()
        {
            this.UIMap.BufferOverflowEmailAltaAgente();
            Assert.Fail("Desbordamiento de buffer");
            //No se puede implementar el Assert hasta que el error haya sido corregido
        }

        [TestMethod]
        public void BufferOverflowTelefonoAltaAgente()
        {
            this.UIMap.BufferOverflowTelefonoAltaAgente();
            Assert.Fail("Desbordamiento de buffer");
        }

        [TestMethod]
        public void BufferOverflowComentarioAltaAgente()
        {
            this.UIMap.BufferOverflowComentarioAltaUsuario();
            Assert.Fail("Desbordamiento de buffer");
            //No se puede implementar el Assert hasta que el error haya sido corregido
        }
        
        [TestMethod]
        public void BufferOverflowUsuarioComisarioAltaAgente()
        {
            this.UIMap.AssertMethodUsuaarioComisario();
            Assert.Fail("Desbordamiento de buffer");
            //No se puede implementar el Assert hasta que el error haya sido corregido
        }**/

        [TestMethod]
        public void ConsultarPlanningComisario()
        {
            this.UIMap.RecordedMethodConsultarPlanningComisario();
            this.UIMap.AssertMethodConsultarPlanningComisario();
        }

        [TestMethod]
        public void ConsultarPlanningPropioAgente()
        {
            this.UIMap.RecordedMethodConsultarPlanningAgente();
            this.UIMap.AssertMethodConsultarPlanningAgente();
        }

        [ClassInitialize()]
        public static void myClassinitialize(TestContext context) 
        {
            string current_path= System.IO.Directory.GetCurrentDirectory();
            int i = current_path.IndexOf("VirtualPolice");
            sut = current_path.Substring(0, i) + "VirtualPolice\\VirtualPolice\\bin\\Debug\\VirtualPolice.exe";
        }

        [TestInitialize()]
        public void mytestinitialize() 
        {
            app = ApplicationUnderTest.Launch(sut);
            app.CloseOnPlaybackCleanup= true; // reiniciará la app tras cada prueba
        }

        [TestCleanup()]
        public void mytestcleanup() 
        {
            app.Dispose();
        }


        #region Atributos de prueba adicionales

        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:

        ////Use TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
        //}

        ////Use TestCleanup para ejecutar el código después de ejecutar cada prueba
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
        //}

        #endregion

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;


    }
}
