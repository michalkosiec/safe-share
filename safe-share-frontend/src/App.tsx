import {Navigate, Route, Routes} from "react-router-dom";
import LoginView from "./features/auth/views/LoginView.tsx";
import RegisterView from "./features/auth/views/RegisterView.tsx";
import VaultView from "./features/vault/views/VaultView.tsx";
import AuthLayout from "./layouts/AuthLayout.tsx"
import MainLayout from "./layouts/MainLayout.tsx";
import {useAuth} from "./features/auth/hooks/useAuth.ts";
import {Spinner} from "./components/Spinner.tsx";
export default function App() {
  const {isAuthenticated, isLoading} = useAuth();
  if (isLoading) {
    return <Spinner fullScreen={true}/>
  }

  return (
    <Routes>
      <Route element={<AuthLayout />}>
        <Route path="/login" element={isAuthenticated ? <Navigate to="/vault" replace /> : <LoginView />} />
        <Route path="/register" element={isAuthenticated ? <Navigate to="/vault" replace /> : <RegisterView />} />
      </Route>
      <Route element={<MainLayout />}>
        <Route path="/vault" element={isAuthenticated ? <VaultView /> : <Navigate to="/login" replace/>}/>
      </Route>
      <Route path="*" element={isAuthenticated ? <Navigate to="/vault" /> : <Navigate to="/login" replace/>} />
    </Routes>
  )
}