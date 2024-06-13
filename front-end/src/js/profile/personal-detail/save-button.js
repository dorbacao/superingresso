import { userService } from "../../services/userService";
import newModal from '../../my-components/modal.js';


function getValue(id){
    try {
        return document.getElementById(id).value;
    } catch (error) {
        return '';
    }    
}

async function savePersonalData(){
    var service = new userService();

    var user = {
        id: window.location.search.replace('?id=', ''),
        nome: getValue('first-name'),
        sobreNome: getValue('last-name'),
        telefone: getValue('phone'),
        endereco: getValue('endereco'),
        codigoPostal: getValue('zip-code'),
        cidade: getValue('city-state'),
        estado: getValue('country'),
        email: getValue('email'),
        telefone: getValue('phone'),
    };

    
    await service.changeUserAsync(user);
}

async function saveButton_Click(){
    let opt = { targetId: 'target-modal', text:'Deseja realmente salvar os novos dados digitados?' };
    var modalManager = newModal(opt)
    var modal = modalManager.createNew();

    modalManager.getConfirmButton().addEventListener('click', async () => {
        await savePersonalData();
        modal.hide();
    });

    modal.show();
}

export async function setupSaveButton(element){

    element.addEventListener('click', saveButton_Click);
}