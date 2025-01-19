import { Route, Routes } from "react-router-dom"
import Login from "./pages/Login"
import Dashboard from "./pages/Dashboard"
import Register from "./pages/Register"

const AppRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path='/' element={<Login/>}/>
            <Route path="/dashboard" element={<Dashboard/>}/>
            <Route path="/register" element={<Register/>}/>
            <Route path="*" element={<h1>Not Found</h1>}/>
        </Routes>
    )
}

export default AppRoutes