import toastr from "../../components/toast";
import { userService } from '../../services/userService';


export async function initChangePassword(){
    document.getElementById("cancelarButton").addEventListener('click', ()=>{
        document.getElementById('new-password').value ='';
        document.getElementById('confirm-password').value ='';

        document.getElementById('new-password').focus();
    });


    document.getElementById("alterarSenhaButton").addEventListener('click', async ()=>{
        var newPassword = document.getElementById('new-password');
        var confirmPassword = document.getElementById('confirm-password');
        newPassword.style.border = '';
        confirmPassword.style.border = '';

        if(newPassword.value == ''){
            
            toastr.error("Os campos 'Senha'é obrigatórios.");
            newPassword.focus();
            newPassword.style.border = '1px solid #ef3c59';
            return;
        }


        if(confirmPassword.value == ''){
            
            toastr.error("Os campos 'Confirmar Senha'é obrigatórios.");
            confirmPassword.style.border = '1px solid #ef3c59';
            confirmPassword.focus();
            return;
        }

        if(newPassword.value != confirmPassword.value){
            
            toastr.error("Os campos 'Senha' e 'Confirmar Senha' devem ser iguais.");
            return;
        }

        var service = new userService();

        var id = window.location.search.replace("?id=", "");

        var user = await service.changePassword(id, newPassword.value, confirmPassword.value);

        if(user){
            toastr.success("Senha alterada com sucesso!");
            newPassword.value = '';
            confirmPassword.value = '';
        }        
    });


}
