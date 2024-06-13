import { userService } from '../../services/userService';


export async function initDeleteAccount(){
    document.getElementById("apagarContaButton").addEventListener('click', ()=>{
        alert('apagar conta');
    });
}