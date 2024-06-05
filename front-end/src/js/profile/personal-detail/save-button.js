import { userService } from "../../services/userService";


function getValue(id){
    try {
        return document.getElementById(id).value;
    } catch (error) {
        return '';
    }    
}
export async function setupSaveButton(element){

    element.addEventListener('click', async ()=>{
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
    });

}