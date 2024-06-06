
export async function initProfileHeader(){
    
    
    document.getElementById('signoutButton').addEventListener('click', async ()=>{
        window.location = 'signout.html';
    });

    document.getElementById('configuracoesLink').addEventListener('click', async ()=>{
        alert('configuracoesLink');
    });

    
}
setTimeout(initProfileHeader, 500);