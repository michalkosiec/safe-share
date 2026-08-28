import {Navigate, Route, Routes} from "react-router-dom";
import LoginView from "./features/auth/views/LoginView.tsx";
import RegisterView from "./features/auth/views/RegisterView.tsx";
import VaultView from "./features/vault/views/VaultView.tsx";
import AuthLayout from "./layouts/AuthLayout.tsx"
import MainLayout from "./layouts/MainLayout.tsx";

export default function App() {
  return (<Routes>
    <Route path="/" element={<Navigate to="/login" replace/>} />

    <Route element={<AuthLayout />}>
      <Route path="/login" element={<LoginView />} />
      <Route path="/register" element={<RegisterView />} />
    </Route>
    <Route element={<MainLayout />}>
      <Route path="/vault" element={<VaultView />} />
    </Route>
  </Routes>)
}