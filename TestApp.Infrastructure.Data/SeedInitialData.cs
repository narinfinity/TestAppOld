using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApp.Infrastructure.Data
{
    public static class SeedInitialDataExtention
    {
        public static void SeedInitialData(this MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Categories]
                                   ([Name])
                             VALUES
                                   ('Organic'),
                                   ('Meat'),
                                   ('Fish & Seafood'),
                                   ('Frozen'),
                                   ('Sweets')");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[Products]
           ([CategoryId]
           ,[Description]
           ,[Name]
           ,[Price]
           ,[Url])
     VALUES
           (1
           ,'A is for apple―and a lot of pesticides. According to the Food and Drug Administration, more pesticides (a whopping 36) are found on apples than on any other fruit or vegetable. In one test, as many as seven chemicals were detected on a single apple.'
           ,'Apple'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/food-recipes/shopping-storing/0108/organic-red-apple_300.jpg')

		   ,(1
           ,'The grains that dairy cows eat are heavily treated with chemicals, which have a residual, though still notable, presence in milk and dairy products. (Milk may also contain bovine growth hormone and antibiotics.)'
           ,'Butter and Milk'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/tips/milk-carton_300.jpg')
		   
		   ,(1
           ,'Cantaloupes often contain five of the longest-lasting chemicals, one of which is dieldrin, an exceedingly toxic and carcinogenic insecticide. Though it was banned in 1974, residues still persist in soils and are taken up through the cantaloupe''s roots and absorbed into the edible portion'
           ,'Cantaloupe'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/food-recipes/shopping-storing/0108/organic-canteloupe_300.jpg')

		   ,(1
           ,'In a survey of 42 common vegetables, cucumbers were ranked second in cancer risk and 12th in “most contaminated food” by the Environmental Working Group, a respected public-interest group.'
           ,'Cucumber'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/food-recipes/shopping-storing/0108/organic-cucumber_300.jpg')

		   ,(1
           ,'Because grapes ripen quickly, tend to mold, and attract insects, growers hit them with multiple applications of various chemicals. The worst are Chilean grapes, which are treated with as many as 17 of them. (Ninety percent of the grapes eaten in the United States from January to April are Chilean.) '
           ,'Grape'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/food-recipes/shopping-storing/0108/organic-grapes_300.jpg')

		   ,(1
           ,'The Environmental Protection Agency has more than 60 pesticides registered for use on green beans.'
           ,'Green Bean'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/tips/green-beans_300.jpg')

		   ,(1
           ,'In a certain cartoon, spinach makes muscles. In real life, the chemicals used to treat it may cause cancer or interfere with hormone production.'
           ,'Spinach'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/food-recipes/shopping-storing/0108/organic-spinach_300.jpg')

		   ,(1
           ,'Strawberries are one of the most contaminated of all produce items in the United States. '
           ,'Strawberry'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/tips/strawberries_300.jpg')

		   ,(1
           ,'Like cantaloupes and cucumbers, winter squash has a propensity to absorb dieldrin from the soil into its edible parts. '
           ,'Winter Squash'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//cdn-image.realsimple.com/sites/default/files/styles/rs_main_image/public/image/images/food-recipes/shopping-storing/0108/organic-butter-squash_300.jpg')

		   ,(2
           ,'Premium five-star steakhouse quality Free range, humanely raised animals. Produced without any GMOs, antibiotics, growth hormones, or pesticides. Preservative and gluten free. Optimal ratio of Omega-6 and Omega-3, contains more Omega-3, beta-carotene and vitamin E than any other beef'
           ,'Beef'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.americanfarmersnetwork.com/media/catalog/product/cache/7/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/b/o/boneless_ribeyes_1.jpg')

		   ,(2
           ,'Free range, humanely raised animals. Produced without antibiotics, synthetic hormones, or pesticides in animals’ feed. Preservative and gluten free. Low fat and lean – healthiest and most nutritious meat possible. Individually wrapped – choose exact amount you need and leave the rest for later'
           ,'Chicken'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.americanfarmersnetwork.com/media/catalog/product/cache/7/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/o/r/organic_chicken_breast.jpg')

		   ,(2
           ,'Pigs are raised without any antibiotics, synthetic hormones, chemical pesticides, or chemical fertilizers. Our animals are raised on small family farms, are fed no animal by-products, and only consume a certified organic, vegetarian, non-GMO diet. Discover the drastic difference in taste, quality, and nutrition that separates our pork from all of the rest!'
           ,'Pork'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.americanfarmersnetwork.com/media/catalog/product/cache/7/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/l/o/louisiana-cajun-style-pork-chop-__dsc0117-2__1.jpg')

		   ,(2
           ,'Turkeys are raised without any antibiotics, synthetic hormones, or pesticides. These turkeys are raised on a small family farm, are fed no animal by-products, and consume only certified organic diet. Processing at an optimal age ensures the best tenderness in meat. Each bird is tracked from egg to your door. Discover a delicious taste difference that separates our turkeys from all of the others.'
           ,'Turkey'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.americanfarmersnetwork.com/media/catalog/product/cache/7/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/1/0/100093-2_1.jpg')

		   ,(3
           ,'Anchovies are small, green fish with blue reflections due to a silver-colored longitudinal stripe that runs from the base of the caudal fin. They range from 2 to 40 cm (0.79 to 15.75 in) in adult length,'
           ,'Anchovy'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Anchovy_closeup.jpg/220px-Anchovy_closeup.jpg')

		   ,(3
           ,'The bream is usually 30 to 55 cm (12 to 22 in) long, though some specimens of 75 cm (30 in) have been recorded; it usually weighs 2 to 4 kg (4.4 to 8.8 lb). The maximum length is 90 cm (35.5 in) with a maximum recorded weight of about 9.1 kg (20 lb).'
           ,'Bream'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/a/a7/Bream.jpg/300px-Bream.jpg')

		   ,(3
           ,'They are typically small to medium in size, although a few species can reach lengths of greater than 100 cm (39 in).'
           ,'Takifugu rubripes'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/2/25/Fugu_in_Tank.jpg/329px-Fugu_in_Tank.jpg')

		   ,(3
           ,'The bluefish is a moderately proportioned fish, with a broad, forked tail. The spiny first dorsal fin is normally folded back in a groove, as are its pectoral fins. Coloration is a grayish blue-green dorsally, fading to white on the lower sides and belly. Its single row of teeth in each jaw is uniform in size, knife-edged, and sharp.'
           ,'Bluefish'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/d/de/Pomatomus_saltatrix.png/220px-Pomatomus_saltatrix.png')

		   ,(3
           ,'Channel catfish possess very keen senses of smell and taste.'
           ,'Channel catfish'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/d/d8/Channelcat.jpg/220px-Channelcat.jpg')

		   ,(3
           ,'The Squaliformes are an order of sharks that includes about 126 species in seven families. Members of the order have two dorsal fins, which usually possess spines, no anal fin or nictitating membrane, and five gill slits. In most other respects, however, they are quite variable in form and size. They are found worldwide, from polar to tropical waters, and from shallow coastal seas to the open ocean.'
           ,'Squaliformes'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/1/12/Spiny_dogfish.jpg/250px-Spiny_dogfish.jpg')

		   ,(3
           ,'Sparus aurata is a fish of the bream family Sparidae found in the Mediterranean Sea and the eastern coastal regions of the North Atlantic Ocean. It commonly reaches about 35 centimetres (1.15 ft) in length, but may reach 70 cm (2.3 ft) and weigh up to about 7.36 kilograms.'
           ,'Sparus aurata'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/9/95/Sparus_aurata_Sardegna.jpg/220px-Sparus_aurata_Sardegna.jpg')
		   
		   ,(3
           ,'Eels are elongated fish, ranging in length from 5 cm (2.0 in) in the one-jawed eel (Monognathus ahlstromi) to 4 m (13 ft) in the slender giant moray.'
           ,'Eel'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/9/94/Rostrata.jpg/284px-Rostrata.jpg')

		   ,(3
           ,'Flounders are a group of flatfish species. They are demersal fish found at the bottom of oceans around the world; some species will also enter estuaries.'
           ,'Flounder'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/2/24/Flounder_hawaii.jpg/250px-Flounder_hawaii.jpg')

		   ,(3
           ,'Groupers are teleosts, typically having a stout body and a large mouth. They are not built for long-distance, fast swimming. They can be quite large, and lengths over a meter and weights up to 100 kg are not uncommon.'
           ,'Grouper'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/2/29/Epinephelus_malabaricus.jpg/250px-Epinephelus_malabaricus.jpg')

		   ,(3
           ,'Halibut are often boiled, deep-fried or grilled while fresh. Smoking is more difficult with halibut meat than it is with salmon, due to its ultra-low fat content. Eaten fresh, the meat has a clean taste and requires little seasoning. Halibut is noted for its dense and firm texture.'
           ,'Halibut'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/2/20/Pacific_Halibut.JPG/220px-Pacific_Halibut.JPG')

		   ,(3
           ,'Lingcod are endemic to the west coast of North America, with the center of abundance off the coast of British Columbia. They are found on the bottom, with most individuals occupying rocky areas at depths of 10 to 100 m (32 to 328 ft). Tagging studies have shown lingcod are a largely nonmigratory species, with colonization and recruitment occurring in localized areas only.'
           ,'Lingcod'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/0/01/Lingcod1.JPG/220px-Lingcod1.JPG')

		   ,(3
           ,'Mullets are distinguished by the presence of two separate dorsal fins, small triangular mouths, and the absence of a lateral line organ. They feed on detritus, and most species have unusually muscular stomachs and a complex pharynx to help in digestion.'
           ,'Mullet'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Mugil_cephalus_Minorca.jpg/250px-Mugil_cephalus_Minorca.jpg')

		   ,(3
           ,'The orange roughy is not a vertically slender fish. Its rounded head is riddled with muciferous canals (part of the lateral line system), as is typical of slimeheads.'
           ,'Orange roughy'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/d/db/Hoplostethus_atlanticus_02_Pengo.jpg/220px-Hoplostethus_atlanticus_02_Pengo.jpg')

		   ,(3
           ,'Esox is a genus of freshwater fish, the only living genus in the family Esocidae—the esocids which were endemic to North America and Eurasia during the Paleogene through present.'
           ,'Esox'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/7/7f/Esox_hdm.JPG/220px-Esox_hdm.JPG')

		   ,(3
           ,'Pollock can grow to 105 centimetres (3.44 ft) and can weigh up to 21 kilograms (46 lb). P. virens has a strongly defined, silvery lateral line running down the sides. Above the lateral line, the color is a greenish black. The belly is white, while P. pollachius has a distinctly crooked lateral line, grayish to golden belly, and a dark brown back.'
           ,'Pollock'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Pollachius_pollachius_aquarium.jpg/260px-Pollachius_pollachius_aquarium.jpg')

		   ,(3
           ,'Salmon is a popular food. Classified as an oily fish, salmon is considered to be healthy due to the fish''s high protein, high omega-3 fatty acids, and high vitamin D content. Salmon is also a source of cholesterol, with a range of 23–214 mg/100 g depending on the species.'
           ,'Salmon'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/7/73/Atlantic_salmon_Atlantic_fish.jpg/220px-Atlantic_salmon_Atlantic_fish.jpg')

		   ,(3
           ,'Tuna and mackerel sharks are the only species of fish that can maintain a body temperature higher than that of the surrounding water. An active and agile predator, the tuna has a sleek, streamlined body, and is among the fastest-swimming pelagic fish – the yellowfin tuna, for example, is capable of speeds of up to 75 km/h (47 mph).'
           ,'Tuna'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/2/21/Tuna_assortment.png/250px-Tuna_assortment.png')

		   ,(3
           ,'Coregonus is a diverse genus of fish in the salmon family (Salmonidae). The Coregonus species are known as whitefishes. The genus contains at least 68 described extant taxa, but the true number of species is a matter of debate. The type species of the genus is Coregonus lavaretus.'
           ,'Coregonus'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*1000) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Coregonus_lavaretus_maraena_1.jpg/220px-Coregonus_lavaretus_maraena_1.jpg')

		   ,(4
           ,'Large short rib empanada'
           ,'Large short rib empanada'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*700) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/uploads/2015/11/SLIDE2.jpg')

		   ,(4
           ,'.9oz black bean empanada'
           ,'.9oz black bean empanada'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*700) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/uploads/2016/10/chicken-breast-banner1.jpg')

		   ,(4
           ,'Mini spring cupcakes'
           ,'Mini spring cupcakes'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*700) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/uploads/2015/11/SLIDE1.jpg')

		   ,(4
           ,'Crispy wrapped shrimp'
           ,'Crispy wrapped shrimp'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/uploads/2015/11/SLIDE2.jpg')

		   ,(4
           ,'Individual flan'
           ,'Individual flan'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/uploads/2016/10/chicken-breast-banner1.jpg')

		   ,(4
           ,'Stuffed cheddar bacon burger'
           ,'Stuffed cheddar bacon burger'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/uploads/2015/11/SLIDE1.jpg')

		   ,(4
           ,'Large black bean empanada'
           ,'Large black bean empanada'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/uploads/2015/11/SLIDE2.jpg')

		   ,(4
           ,'Large short rib empanada'
           ,'Large short rib empanada'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/GK10.jpg')

		   ,(4
           ,'.9oz black bean empanada'
           ,'.9oz black bean empanada'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/GK09.jpg')

		   ,(4
           ,'Crispy wrapped shrimp'
           ,'Crispy wrapped shrimp'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/HA00.jpg')

		   ,(4
           ,'Diced turkey'
           ,'Diced turkey'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/TIP2.jpg')

		   ,(4
           ,'Potato knish'
           ,'Potato knish'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/AM12.jpg')

		   ,(4
           ,'Uc chicken breast filet 5oz'
           ,'Uc chicken breast filet 5oz'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/GK11.jpg')

		   ,(4
           ,'Fully cooked bbq chicken filet'
           ,'Fully cooked bbq chicken filet'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/GK10.jpg')

		   ,(4
           ,'Raw crispy wings sections'
           ,'Raw crispy wings sections'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*500) - 2.5 + 1))
           ,'//www.jamacfoods.com/wp-content/plugins/ultimate-product-catalogue/images/GK09.jpg')

		   ,(5
           ,'Milk chocolate with 39 % cocoa and the alluring aroma that results from the use of vanilla pods: these are the secrets in this stupendous milk chocolate bar from Luca Montersino! Have a nibble whenever you please for a well earned recharge and a tasty snack!'
           ,'Milk Chocolate Bar'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//media.eataly.net/media/catalog/product/cache/8/small_image/303x/9df78eab33525d08d6e5fb8d27136e95/g/o/golosi-di-salute-barretta-latte-75g.jpg')

		   ,(5
           ,'Fruit-shaped hard candy is a common type of sugar candy, containing sugar, color, flavor, and a tiny bit of water.'
           ,'Fruit-shaped hard candy'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/1/13/HardCandy.jpg/120px-HardCandy.jpg')

		   ,(5
           ,'Kompeitō is a traditional Japanese sugar candy. When finished, it is almost 100% sugar.'
           ,'Kompeitō'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/5/54/Kompeito_konpeito.JPG/120px-Kompeito_konpeito.JPG')

		   ,(5
           ,'Chikki are homemade nut brittles popular in India. Between the nuts or seeds is hard sugar candy.'
           ,'Chikki'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Chikki_assortment.jpg/120px-Chikki_assortment.jpg')

		   ,(5
           ,'Haribo gummy bears were the first gummi candy ever made. They are soft and chewy.'
           ,'Haribo'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/6/66/Gummy_bears.jpg/120px-Gummy_bears.jpg')

		   ,(5
           ,'Made with cashew nuts and milk, this barfi is a childhood favourite of many. So easy and yet so divine, no Indian celebration is complete without it.'
           ,'Zafrani Kaju ki Katli'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//i.ndtvimg.com/i/2015-07/sweet-625_625x350_51438261999.jpg')

		   ,(5
           ,'Bask in the glory of every decadent dinner party with this absolute crowd-pleaser.'
           ,'Chocolate Lava Cake'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//i.ndtvimg.com/i/2015-07/sweet-625_625x350_71438262734.jpg')

		   ,(5
           ,'So light and fluffy, it just doesn''t get better than this.'
           ,'Guilt-Free Dark Chocolate Mousse'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//i.ndtvimg.com/i/2015-07/sweet-625_625x350_71438262187.jpg')

		   ,(5
           ,'Stuffed with almonds and cashews, these 30-minute ladoos are your dream come true'
           ,'Nariyal Ladoo'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//i.ndtvimg.com/i/2015-07/sweet-625_625x350_61438262263.jpg')

		   ,(5
           ,'Pantteri is a soft, chewy Finnish sugar candy. The colored ones are fruity, while black are salmiakki (salty liquorice-flavored).'
           ,'Pantteri'
           ,CONVERT(float, 2.58 + Rand()*((10 + Rand()*50) - 2.5 + 1))
           ,'//upload.wikimedia.org/wikipedia/commons/thumb/7/78/Pantteri_Mix.jpg/120px-Pantteri_Mix.jpg')");


        }

    }
}
