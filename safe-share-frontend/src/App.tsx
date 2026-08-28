import {Navigate, Route, Routes} from "react-router-dom";
import LoginView from "./features/auth/views/LoginView.tsx";
import RegisterView from "./features/auth/views/RegisterView.tsx";
import VaultView from "./features/vault/views/VaultView.tsx";

export default function App() {
  return (<Routes>
    <Route path="/" element={<Navigate to="/login" replace/>} />
    <Route path="/login" element={<LoginView />} />
    <Route path="/register" element={<RegisterView />} />
    <Route path="/vault" element={<VaultView />} />
  </Routes>)
}