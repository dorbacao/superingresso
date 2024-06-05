import toastr from 'toastr';
import { userService } from '../../services/userService';
import {setupSaveButton} from './save-button';
import { setupCancelButton } from './cancel-button';

function setInput(id, value){
    document.getElementById(id).value = value;
}

export async function initPersonalDetail(){
    if(!window.location.search.includes("id=")){
        var login = JSON.parse(localStorage.getItem('login'));
        window.location = `profile.html?id=${login.id}`
    }
    else{
        var id = window.location.search.replace("?id=", "");
        var service = new userService();
        var userDetail = await service.getUserById(id);

        setInput("first-name", userDetail.nome);
        setInput("last-name", userDetail.sobreNome);
        setInput("phone", userDetail.telefone);
        setInput("email", userDetail.email);
        setInput("endereco", userDetail.endereco);
        setInput("city-state", userDetail.cidade);
        setInput("country", userDetail.estado);
        setInput("zip-code", userDetail.codigoPostal);

        console.log(userDetail);
    }
}
await setupSaveButton(document.getElementById('save-user-detail-button'));
await setupCancelButton(document.getElementById('cancel-user-detail-button'));

