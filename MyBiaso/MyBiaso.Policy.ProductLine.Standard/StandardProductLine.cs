using System.Drawing;
using AiFrame.InterfaceLib.Policy.ProductLine;

namespace MyBiaso.Policy.ProductLine.Standard {
    
    /// <summary>
    /// Definition der Standardproduktlinie.
    /// </summary>
    public class StandardProductLine:IProductLine {
        
        /// <summary>
        /// Logo
        /// </summary>
        /// <returns></returns>
        public Image GetLogo() {
            return null;
        }

        public ProductFamily GetProductLine() {
            return ProductFamily.Standard;
        }

        public string ProgramName() {
            return "MyBiaso - My business is a small one";
        }

        public Icon Icon() {
            return null;
        }
    }
}
