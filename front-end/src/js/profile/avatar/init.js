import { userService } from '../../services/userService';

export async function initAvatar() {

    var id = window.location.search.replace("?id=", "");
    var service = new userService();
    var user = await service.getUserById(id);

    var ativoCss = "badge badge-soft-success my-3 inline-block px-4";
    var inativoCss = "badge badge-soft-danger my-3 inline-block px-4";

    document.getElementById('nomeH2').innerText = user.nome + ' ' + user.sobreNome;
    document.getElementById('emailP').innerText = user.email;
    document.getElementById('activeDiv').innerText = user.ativo ? "Ativo" : "Inativo";
    document.getElementById('activeDiv').className = user.ativo ? ativoCss : inativoCss;


}