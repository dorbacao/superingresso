import toastr from "./../components/toast";
export async function initSignout(){
    localStorage.setItem('token', '');
    setTimeout(()=>{window.location = 'login2.html'}, 1500);
}

await initSignout();