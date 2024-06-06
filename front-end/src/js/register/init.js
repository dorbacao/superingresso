
import toastr from 'toastr';
import 'toastr/build/toastr.min.css';
import { authService } from '../services/auth';

function setValidity(input, message){
    if(!input.value){
        toastr.error(message);
        input.setCustomValidity(message);
        input.reportValidity();
        return false;
    }    
    return true;
}

export async function init(){
  
    let loginButton  = document.getElementById('login-button');

    loginButton.addEventListener('click', async ()=>{
        let nomeInput = document.getElementById('nomeInput');
        let emailInput = document.getElementById('emailInput');
        let senhaInput = document.getElementById('senhaInput');
        let confirmarSenhaInput = document.getElementById('confirmarSenhaInput');
        let aceitoCheck = document.getElementById('aceito-check');

        if(!aceitoCheck.checked){
            let msg = 'Você precisa aceitar os termos e condições';
            toastr.error(msg);
            aceitoCheck.setCustomValidity(msg);
            aceitoCheck.reportValidity();
            return;
        }else{
            aceitoCheck.setCustomValidity('');
        }

        let validity = setValidity(confirmarSenhaInput, 'Confirmar senha informada');

        if(!validity) validity = setValidity(senhaInput, 'Senha não informada');

        if(!validity) validity = setValidity(emailInput, 'Email não informado');

        if(!validity) validity = setValidity(nomeInput, 'Nome não informado');        

        if(validity && senhaInput.value != confirmarSenhaInput.value){
            var msg = 'Os campos "Senha" e "Confirmar Senha" não podem ser diferentes';
            toastr.error(msg);
            confirmarSenhaInput.setCustomValidity(msg);
            confirmarSenhaInput.reportValidity();
            validity = false;
        }

        if(!validity) return;

        var newUser = {
            nome:nomeInput.value, 
            email:emailInput.value, 
            senha:senhaInput.value, 
            confirmarSenha:confirmarSenhaInput.value, 
        };

        console.log(newUser);

        var service = new authService();
        let result = await service.createAndLogin(newUser);
    });

    
}

await init();