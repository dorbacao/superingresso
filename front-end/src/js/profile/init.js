import { userService } from "../services/userService";
import 'toastr/build/toastr.min.css';
import {initPersonalDetail} from './personal-detail/init';
import {initChangePassword} from './change-password/init';
import {initDeleteAccount} from './delete-account/init';
import {initPreferences} from './preferences/init';
import { initAvatar } from "./avatar/init";

await initPersonalDetail();
await initChangePassword();
await initDeleteAccount();
await initPreferences();
await initAvatar();

