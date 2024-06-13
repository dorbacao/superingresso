import { userService } from "../../services/userService";
import newModal from '../../my-components/modal.js';
import toastr from "./../../components/toast";

function setInput(id, value) {
    document.getElementById(id).value = value;
}

async function getUserInfo() {
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

export async function setupCancelButton(element) {

    element.addEventListener('click', async () => {

        let opt = { targetId: 'target-modal', text:'Deseja realmente restaurar os dados do usuário?' };
        var modalManager = newModal(opt)
        var modal = modalManager.createNew();

        modalManager.getCancelButton().addEventListener('click', (e) => {

        });

        modalManager.getConfirmButton().addEventListener('click', async () => {
            await getUserInfo();
            modal.hide();
        });

        modal.show();


    });

}