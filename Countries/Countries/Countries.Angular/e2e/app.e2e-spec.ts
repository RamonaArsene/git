import { CountriesAngularPage } from './app.po';

describe('countries-angular App', function() {
  let page: CountriesAngularPage;

  beforeEach(() => {
    page = new CountriesAngularPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
