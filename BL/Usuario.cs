using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EignacioWeeCompanyContext context= new DL.EignacioWeeCompanyContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.Telefono}', '{usuario.Email}', '{usuario.Empresa}'");
                    result.Objects = new List<object>();

                    if (query>0)
                    {
                        result.Message = "Los datos se ingresaron correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }
    }
}