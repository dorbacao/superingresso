import toastr from 'toastr';
export async function initSignout(){
    localStorage.setItem('login', '');
    setTimeout(()=>{window.location = 'login.html'}, 3000);
}

await initSignout();