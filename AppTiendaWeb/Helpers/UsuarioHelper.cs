using Core.Contracts.Service;
using Core.Models.AppTiendaModels;
using Core.Models.AppTiendaWebModels;
using System.Security.Cryptography;

namespace Presentation.AppTiendaWeb.Helpers
{
    public class UsuarioHelper
    {
        #region Password
        // 24 = 192 bits
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;


        public static string HashPassword(string password)
        {
            // http://stackoverflow.com/questions/19957176/asp-net-identity-password-hashing

            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(HashByteSize);
            }
            byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] _passwordHashBytes;

            int _arrayLen = (SaltByteSize + HashByteSize) + 1;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != _arrayLen) || (src[0] != 0))
            {
                return false;
            }

            byte[] _currentSaltBytes = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

            byte[] _currentHashBytes = new byte[HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, _currentSaltBytes, HasingIterationsCount))
            {
                _passwordHashBytes = bytes.GetBytes(SaltByteSize);
            }

            return AreHashesEqual(_currentHashBytes, _passwordHashBytes);

        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
        #endregion

        public static Usuario UsuarioModelViewToModelDb(UsuarioNewModelView newUser)
        {
            return new Usuario
            {
                Activo = newUser.Activo,
                ApellidoMaterno = newUser.Materno,
                ApellidoPaterno = newUser.Paterno,
                Email = newUser.Email,
                Nombre = newUser.Nombre,
                Password = newUser.Password,
                Telefono = newUser.Telefono,
                UsuarioDireccion = new UsuarioDireccion
                {
                    Calle = newUser.Calle,
                    Ciudad = newUser.Ciudad,
                    Colonia = newUser.Colonia,
                    Cp = newUser.Cp,
                    EstadoId = newUser.EstadoId,
                    Numero = newUser.Numero
                }
            };
        }
        public static UsuarioConsultaModelView UsuarioToUsuarioConsultaModelView(Usuario usuario)
        {
            return new UsuarioConsultaModelView
            {
                UsuarioId = usuario.UsuarioId,
                Activo = usuario.Activo.Value,
                Email = usuario.Email,
                Materno = usuario.ApellidoMaterno,
                Nombre = usuario.Nombre,
                Paterno = usuario.ApellidoPaterno,
                Telefono = usuario.Telefono,
                UsuarioDetalle = usuario.UsuarioDireccion != null ? new()
                {
                    Calle = usuario.UsuarioDireccion.Calle,
                    Ciudad = usuario.UsuarioDireccion.Ciudad,
                    Colonia = usuario.UsuarioDireccion.Colonia,
                    Cp = usuario.UsuarioDireccion.Cp,
                    EstadoId = usuario.UsuarioDireccion.EstadoId.Value,
                    Numero = usuario.UsuarioDireccion.Numero
                } : null
            };
        }
        public static async Task<Usuario> TokenToUsuarioAsync(string token, IUsuarioService _usuarioService)
        {
            var userId = AesOperationHelper.DecryptString(token);
            var responseUser = await _usuarioService.GetUsuarioById(userId);
            return responseUser;
        }

        public static Usuario UsuarioConsultaModelViewToView(UsuarioConsultaModelView model, Usuario entity)
        {
            entity.Activo = model.Activo;
            entity.ApellidoMaterno = model.Materno;
            entity.ApellidoPaterno = model.Paterno;
            entity.Email = model.Email;
            entity.Nombre = model.Nombre;
            entity.Telefono = model.Telefono;
            entity.UsuarioDireccion.Ciudad = model.UsuarioDetalle.Ciudad;
            entity.UsuarioDireccion.Cp= model.UsuarioDetalle.Cp;
            entity.UsuarioDireccion.Numero = model.UsuarioDetalle.Numero;
            entity.UsuarioDireccion.Colonia = model.UsuarioDetalle.Colonia;
            entity.UsuarioDireccion.Calle = model.UsuarioDetalle.Calle;
            entity.UsuarioDireccion.EstadoId = model.UsuarioDetalle.EstadoId;

            if(!string.IsNullOrEmpty(model.Password))
                entity.Password = HashPassword(model.Password);

            return entity;
        }
    }
}
