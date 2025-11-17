using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum LoginStatus
    {
        Success,          // Thành công
        UserNotFound,     // Không tìm thấy user
        WrongPassword,    // Sai mật khẩu
        AccessDenied      // Bị từ chối (là Customer)
    }
}
