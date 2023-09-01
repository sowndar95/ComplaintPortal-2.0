import { BrowserRouter } from 'react-router-dom';
import { HelmetProvider } from 'react-helmet-async';
import ThemeProvider from './theme';
import './App.css';
import Router from './routes/routes';

function App() {
  return (
    <HelmetProvider>
      <BrowserRouter>
        <ThemeProvider>          
          <Router/>
        </ThemeProvider>
      </BrowserRouter>
    </HelmetProvider>
  );
}

export default App;
