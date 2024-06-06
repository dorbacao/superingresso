
import toastr from 'toastr';
import 'toastr/build/toastr.min.css';
import { authService } from '../services/auth';

export function loginInit(){

    document.getElementById("loginInput").focus();

    var senhaInput = document.getElementById("senhaInput");

    senhaInput.addEventListener("keypress", function(e) {
        if (e.key === 'Enter') {
            var btn = document.querySelector("#loginInput");
            btn.click();
        }
    });
    

    document.getElementById("loginButton").addEventListener('click', async ()=>{
        var loginInput = document.getElementById("loginInput");
        var senhaInput = document.getElementById("senhaInput");

        var login = loginInput.value;
        var senha = senhaInput.value;

        if(!login){
            loginInput.style.border = '1px solid #ef3c59';
            toastr.error("Login é obrigatório");
            loginInput.focus();
            return;
        }else{
            loginInput.style.border = '';
        }

        if(!senha){
            senhaInput.style.border = '1px solid #ef3c59';;
            toastr.error("Senha é obrigatório");
            senhaInput.focus();
            return;
        }else{
            senhaInput.style.border = '';
        }

       var service = new authService();
       await service.login(login, senha);

    });
}