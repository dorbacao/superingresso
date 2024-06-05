import { userService } from "../../services/userService";
import toastr from 'toastr';

function setInput(id, value){
    document.getElementById(id).value = value;
}

async function getUserInfo(){
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

        toastr.info('Informações do usuário restauradas');

}

export async function setupCancelButton(element){

    var confirmarButton = document.getElementById('confirmar-button');

    confirmarButton.addEventListener('click', async (event)=>{
        await getUserInfo();

        const controlledModal = createModal('#cancel-modal-controlled');
        controlledModal.hide();
    });

    element.addEventListener('click', async ()=>{

        const controlledModal = createModal('#cancel-modal-controlled', {
            keyboard: true, //Boolean. Default is true
            backdrop: false, //Boolean | 'static'. Default is true
        });

        console.log('controlledModal');
        console.log(controlledModal.hide);

        controlledModal.show();

        controlledModal.modal.oncancel = event=>{
            console.log('cancel');
            console.log(event);
        };

        controlledModal.modal.onclose =  event=>{
            console.log('close');
            console.log(event);
        };


    });

}