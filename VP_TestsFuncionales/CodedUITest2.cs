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
    /// Descripción resumida de CodedUITest2
    /// </summary>
    /// 

    [CodedUITest]
    public class CodedUITest2
    {

        static ApplicationUnderTest app;
        static string sut;

        [TestMethod]
        public void FB_AbrirExpediente()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.FB_ModificarExpediente();
            this.UIMap.AssertMethodFB_ModificarExpediente();
        }

        [TestMethod]
        public void FB_ModificarExpediente()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.RecordedMethodFB_ModificarExpediente();
            this.UIMap.AssertMethodFB_ModificarExpediente();
        }

        [TestMethod]
        public void FA3_ModificarExpediente()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.FA3_ModificoExpediente();
            this.UIMap.AssertMethodFA3_ModificarExpediente();
        }

        [TestMethod]
        public void FA8_ModificarExpediente()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.FB_ModificarExpediente();
            this.UIMap.AssertMethodFB_ModificarExpediente();
        }

        [TestMethod]
        public void FB_CrearDenuncia()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.FB_CrearDenuncia();
            this.UIMap.AssertMethodCrearDenuncia();
        }

        [TestMethod]
        public void FA1_DenunciaOnline()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.FA1_DenunciaOnline();
            this.UIMap.AssertMethodFA1_DenunciaOnline();
        }

        [TestMethod]
        public void FA5_CrearDenuncia()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.RecordedMethodFA5_CrearDenuncia();
            this.UIMap.AssertMethodFA5_CrearDenuncia();
        }

        [TestMethod]
        public void FA7_CrearDenuncia()
        {
            this.UIMap.LoginUsuario();
            this.UIMap.RecordedMethodFA7_CrearDenuncia();
            this.UIMap.AssertMethodFA7_CrearDenuncia();
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


        [ClassInitialize()]
        public static void myClassinitialize(TestContext context)
        {
            string current_path = System.IO.Directory.GetCurrentDirectory();
            int i = current_path.IndexOf("VirtualPolice");
            sut = current_path.Substring(0, i) + "VirtualPolice\\VirtualPolice\\bin\\Debug\\VirtualPolice.exe";
        }

        [TestInitialize()]
        public void mytestinitialize()
        {
            app = ApplicationUnderTest.Launch(sut);
            app.CloseOnPlaybackCleanup = true; // reiniciará la app tras cada prueba
        }
        private UIMap map;
    }
}
