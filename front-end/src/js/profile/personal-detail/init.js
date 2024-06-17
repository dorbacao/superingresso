import toastr from "./../../components/toast";
import { userService } from '../../services/userService';
import {setupSaveButton} from './save-button';
import { setupCancelButton } from './cancel-button';

function setInput(id, value){
    document.getElementById(id).value = value;
}

export async function initPersonalDetail(){
    if(!window.location.search.includes("id=")){
        var token = JSON.parse(localStorage.getItem('token'));
        window.location = `profile.html?id=${token.identity.userId}`
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

        $bus.publish('user-loaded', { data: userDetail });        
    }
}
await setupSaveButton(document.getElementById('save-user-detail-button'));
await setupCancelButton(document.getElementById('cancel-user-detail-button'));

