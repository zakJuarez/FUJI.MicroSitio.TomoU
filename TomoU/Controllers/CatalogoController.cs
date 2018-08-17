using System.Linq;
using System.Web.Http;

namespace TomoU.Controllers
{
    [RoutePrefix("api/catalogos")]
    public class CatalogoController : ApiController
    {
        /// <summary>
        /// Permite obtener el listado de 
        /// estados o regiones de un determinado país
        /// </summary>
        /// <param name="pais"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerRegiones")]
        public IQueryable<object> ObtenerRegiones()
        {
            return _context.tbl_CAT_Estado.Where(i => (bool)i.bitActivo).Select(r => new { r.intEstadoID, r.vchEstado });
        }

        private App_Data.TomoUDBEntities _context = new App_Data.TomoUDBEntities();
        /// <summary>
        /// Libera los recursos de la base de datos
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}