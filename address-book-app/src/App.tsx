import React from "react";
import {BrowserRouter } from "react-router-dom";
import { ThemeProvider } from "@emotion/react";
import theme from "./styles/theme";
import AppRoutes from "./routes";


const App: React.FC = () => {
    return (
        <ThemeProvider theme={theme}>
            <BrowserRouter>
                <AppRoutes/>
            </BrowserRouter>
        </ThemeProvider>
    );
}

export default App;